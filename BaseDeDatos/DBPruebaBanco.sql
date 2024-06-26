CREATE DATABASE BancoDb;
GO
USE [BancoDb]
GO
CREATE TABLE [dbo].[Tarjetas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreTitular] [nvarchar](max) NULL,
	[NumeroTarjeta] [nvarchar](max) NULL,
	[MontoOtorgado] [float] NULL,
	[PorcentajeInteres] [float] NULL,
	[PorcentajeInteresMinimo] [float] NULL,
 CONSTRAINT [PK_Tarjetas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransaccionesTarjeta]    Script Date: 25/6/2024 14:32:05 ******/
CREATE TABLE [dbo].[TransaccionesTarjeta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaTransaccion] [datetime2](7) NULL,
	[Monto] [float] NULL,
	[TipoTransaccion] [nvarchar](max) NULL,
	[IdTarjeta] [int] NOT NULL,
 CONSTRAINT [PK_TransaccionesTarjeta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TransaccionesTarjeta]  WITH CHECK ADD  CONSTRAINT [FK_TransaccionesTarjeta_Tarjetas_IdTarjeta] FOREIGN KEY([IdTarjeta])
REFERENCES [dbo].[Tarjetas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TransaccionesTarjeta] CHECK CONSTRAINT [FK_TransaccionesTarjeta_Tarjetas_IdTarjeta]
GO
USE [BancoDb]
GO
insert into Tarjetas(NombreTitular, NumeroTarjeta, MontoOtorgado, PorcentajeInteres, PorcentajeInteresMinimo)values ('Juan Perez', '4521 1102 2987 9001', 2000, 25, 5);
GO
insert into Tarjetas(NombreTitular, NumeroTarjeta, MontoOtorgado, PorcentajeInteres, PorcentajeInteresMinimo)values ('Paolo Navarro', '5012 2102 9060 9131', 1500, 40, 5);
GO
INSERT INTO [dbo].[TransaccionesTarjeta]
           ([Descripcion], [FechaTransaccion], [Monto], [TipoTransaccion], [IdTarjeta])
     VALUES
           ('Camisa Polo', '2024-06-25 10:00:00', 10, 0, 1)
GO

INSERT INTO [dbo].[TransaccionesTarjeta]
           ([Descripcion], [FechaTransaccion], [Monto], [TipoTransaccion], [IdTarjeta])
     VALUES
           ('Cena en Restaurante', '2024-06-22 19:45:00', 30, 0, 1)
GO

INSERT INTO [dbo].[TransaccionesTarjeta]
           ([Descripcion], [FechaTransaccion], [Monto], [TipoTransaccion], [IdTarjeta])
     VALUES
           ('Zapatos Deportivos', '2024-06-24 12:30:00', 50, 0, 2)
GO

INSERT INTO [dbo].[TransaccionesTarjeta]
           ([Descripcion], [FechaTransaccion], [Monto], [TipoTransaccion], [IdTarjeta])
     VALUES
           ('PagoTC', '2024-06-23 15:00:00', 20, 1, 1)
GO

/****** Object:  StoredProcedure [dbo].[sp_EstadoDeCuenta]    Script Date: 25/6/2024 14:32:05 ******/

CREATE PROCEDURE [dbo].[sp_EstadoDeCuenta]
    @NumeroTarjeta VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    -- Variables a utilizar
    DECLARE @SaldoTotal MONEY = 0,
            @InteresBonificable MONEY = 0,
            @CuotaMinima MONEY = 0,
            @TotalContadoConInteres MONEY = 0,
            @SaldoDisponible MONEY = 0,
            @MontoOtorgado MONEY,
			@AbonosTotal MONEY = 0,
			@ComprasTotal MONEY = 0,
			@ComprasMesAnterior MONEY = 0,
			@PorceInteres DECIMAL(12,2),
            @PorcentajeSaldoMinimo DECIMAL(12,2),
            @IdTarjeta INT;
  
    -- Obtención del IdTarjeta, porcentajes y monto otorgado de la tarjeta
    SELECT @IdTarjeta = Id,
           @PorceInteres = PorcentajeInteres, 
           @PorcentajeSaldoMinimo = PorcentajeInteresMinimo, 
           @MontoOtorgado = MontoOtorgado
    FROM Tarjetas
    WHERE NumeroTarjeta = @NumeroTarjeta;

    -- Validar si se obtuvo el IdTarjeta
    IF @IdTarjeta IS NULL
    BEGIN
        RAISERROR ('NumeroTarjeta no encontrado', 16, 1);
        RETURN;
    END

    -- Obtención del saldo actual
    SELECT @ComprasTotal = ISNULL(SUM(Monto), 0)
    FROM TransaccionesTarjeta
    WHERE IdTarjeta = @IdTarjeta
	and TipoTransaccion = 0 
	AND MONTH(FechaTransaccion) = MONTH(GETDATE()) 
	AND YEAR(FechaTransaccion) = YEAR(GETDATE());

	--Obtención de los abonos actuales
	SELECT @AbonosTotal = ISNULL(SUM(Monto), 0)
    FROM TransaccionesTarjeta
    WHERE IdTarjeta = @IdTarjeta
	and TipoTransaccion = 1
	AND MONTH(FechaTransaccion) = MONTH(GETDATE()) 
	AND YEAR(FechaTransaccion) = YEAR(GETDATE());

	-- Obtención de las compras del mes anterior
    SELECT @ComprasMesAnterior = ISNULL(SUM(Monto), 0)
    FROM TransaccionesTarjeta
    WHERE IdTarjeta = @IdTarjeta
      AND TipoTransaccion = 0 
      AND MONTH(FechaTransaccion) = MONTH(DATEADD(MONTH, -1, GETDATE())) 
      AND YEAR(FechaTransaccion) = YEAR(DATEADD(MONTH, -1, GETDATE()));

	SET @SaldoTotal = @ComprasTotal - @AbonosTotal;

    -- Cálculo de valores para compras
    IF @SaldoTotal <> 0
    BEGIN
        SET @CuotaMinima = (@SaldoTotal * @PorcentajeSaldoMinimo) / 100;
        SET @InteresBonificable = (@SaldoTotal * @PorceInteres) / 100;
        SET @TotalContadoConInteres = @SaldoTotal + @InteresBonificable;
    END

    SET @SaldoDisponible = @MontoOtorgado - @SaldoTotal;

    -- Impresión de los resultados
    SELECT *, 
           FORMAT(@SaldoTotal, '0.00') AS SaldoTotal, 
           FORMAT(@CuotaMinima, '0.00') AS CuotaMinima, 
           FORMAT(@InteresBonificable, '0.00') AS InteresBonificable, 
           FORMAT(@TotalContadoConInteres, '0.00') AS TotalContadoConInteres, 
           FORMAT(@SaldoDisponible, '0.00') AS SaldoDisponible,
		   FORMAT(@ComprasTotal, '0.00') AS ComprasTotal,
		   FORMAT(@ComprasMesAnterior, '0.00') AS ComprasMesAnterior
    FROM Tarjetas 
    WHERE NumeroTarjeta = @NumeroTarjeta;
END;
GO
