using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaBanco.WebApi.Data;
using PruebaBanco.WebApi.Models.Tarjetas;
using PruebaBanco.WebApi.ViewModels;
using System.Data;

namespace PruebaBanco.WebApi.Repository.Tarjeta
{
    public class TarjetaRepository : ITarjetaRepository
    {
        private readonly BancoDbContext _context;


        public TarjetaRepository(BancoDbContext context)
        {
            _context = context;

        }

        //Trae todas las tarjetas existentes
        public async Task<List<TarjetasEntity>> GetAll()
        {
            return await _context.Tarjetas.ToListAsync();
        }


        //Sirve para traer el estado de la cuenta
        public async Task<TarjetaInfoViewModel> GetByNumeroTarjeta(string numeroTarjeta)
        {
            return await GetTarjetaInfoByNumeroTarjeta(numeroTarjeta);
        }


        
        private async Task<TarjetaInfoViewModel> GetTarjetaInfoByNumeroTarjeta(string numeroTarjeta)
        {
            var tarjetaEntity = new TarjetaInfoViewModel();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_EstadoDeCuenta";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@NumeroTarjeta", numeroTarjeta));

                await _context.Database.OpenConnectionAsync();

                await using (var result = await command.ExecuteReaderAsync())
                {
                    if (await result.ReadAsync())
                    {
                        tarjetaEntity.Id = result.GetInt32(result.GetOrdinal("Id"));
                        tarjetaEntity.NombreTitular = result.GetString(result.GetOrdinal("NombreTitular"));
                        tarjetaEntity.NumeroTarjeta = result.GetString(result.GetOrdinal("NumeroTarjeta"));
                        tarjetaEntity.MontoOtorgado = result.GetDouble(result.GetOrdinal("MontoOtorgado"));
                        tarjetaEntity.PorcentajeInteres = result.GetDouble(result.GetOrdinal("PorcentajeInteres"));
                        tarjetaEntity.PorcentajeInteresMinimo = result.GetDouble(result.GetOrdinal("PorcentajeInteresMinimo"));

                        // Valores calculados por el procedimiento almacenado
                        tarjetaEntity.SaldoTotal = double.Parse(result.GetString(result.GetOrdinal("SaldoTotal")));
                        tarjetaEntity.CuotaMinima = double.Parse(result.GetString(result.GetOrdinal("CuotaMinima")));
                        tarjetaEntity.InteresBonificable = double.Parse(result.GetString(result.GetOrdinal("InteresBonificable")));
                        tarjetaEntity.TotalContadoConInteres = double.Parse(result.GetString(result.GetOrdinal("TotalContadoConInteres")));
                        tarjetaEntity.SaldoDisponible = double.Parse(result.GetString(result.GetOrdinal("SaldoDisponible")));
                        tarjetaEntity.ComprasTotal = double.Parse(result.GetString(result.GetOrdinal("ComprasTotal")));
                        tarjetaEntity.ComprasMesAnterior = double.Parse(result.GetString(result.GetOrdinal("ComprasMesAnterior")));
                    }
                }
            }

            return tarjetaEntity;
        }


        //Sirve para el encabezado de algunas pantallas
        public async Task<TarjetasEntity> GetTarjetaByIdAsync(int idTarjeta)
        {
            return await _context.Tarjetas.FirstOrDefaultAsync(t => t.Id == idTarjeta);
        }

        
    }

}