using Sneaker_Store.Model;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Sneaker_Store.Services
{
    public interface ISkoRepository
    {
        Sko Add(Sko newSko);
        List<Sko> GetAll();
        Sko GetById(int skoid);
        Sko ReadSko(SqlDataReader reader);
    }
}