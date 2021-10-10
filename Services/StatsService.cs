using System.Data;
using MySql.Data.MySqlClient;
using vtb_backend.Models;

namespace vtb_backend.Services
{
    public static class StatsService
    {
        public static UserStats GetStats(string token)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand(
                $"select * from accounts join userstats on accounts.user_id=userstats.user_id where hash=\"{token}\"");
            var reader = dbase.GetReader(cmd);
            reader.Read();
            var res = new UserStats
            {
                casesDone = reader.GetInt32("case_done"), isQualified = reader.GetBoolean("invester_status"),
                lessonsDone = reader.GetInt32("lessons_done"), money = reader.GetInt32("money"),
                score = reader.GetInt32("score")
            };
            dbase.Close();
            return res;
        }

        public static void SetStats(ApiUserStats stats)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand(
                $"update userstats join accounts on userstats.user_id=accounts.user_id set case_done={stats.casesDone}, lessons_done={stats.lessonsDone}, score={stats.score}, invester_status={stats.isQualified}, money={stats.money} join userstats on accounts.user_id=userstats.user_id where hash={stats.token}");
            dbase.InsertCommand(cmd);
            dbase.Close();
        }
    }
}