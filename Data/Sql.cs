using System.Data.SqlClient;

namespace WebSql.Data
{
    public class Sql
    {
        public static string ConnectionString = "string";
      
        public static void Insert(YT yt)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "INSERT INTO Songs VALUES (@Song, @Video)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Song", yt.Song);
                cmd.Parameters.AddWithValue("@Video", yt.Video);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static void Delete(YT yt)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            { 
                con.Open();
                string query = "DELETE FROM Songs WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", yt.Id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static List<YT> Load()
        {
            List<YT> yt = new List<YT>();
            if (yt.Count > 0)
            {
                yt.Clear();
            }

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "SELECT * FROM Songs";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    YT Video = new YT();
                    Video.Id = reader.GetInt32(0);
                    Video.Song = reader.GetString(1);
                    Video.Video = reader.GetString(2);
                    yt.Add(Video);
                    Console.WriteLine($"{reader.GetInt32(0)}\t{reader.GetString(1)}\t{reader.GetString(2)}");
                }
                con.Close();
            }
            foreach (var item in yt)
            {
                Console.WriteLine($"[{item.Id}] {item.Song} {item.Video} ");
            }
            return yt;
        }

    }
}
