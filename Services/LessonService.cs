using MySql.Data.MySqlClient;
using vtb_backend.Models;

namespace vtb_backend.Services
{
    public static class LessonService
    {
        public static Lesson getLesson(int lessonId)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand(
                $"select step_count from lesson_table where lesson_id={lessonId};" +
                $"select text,step_id ");
            var reader = dbase.GetReader(cmd);
            reader.Read();
            
            //TODO
            return null;
        }
    }
}