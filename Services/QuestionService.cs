using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using vtb_backend.Models;

namespace vtb_backend.Services
{
    public static class QuestionService
    {
        public static Header getDayHeader(int day, int header_id)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand(
                $"select header_id,day_name from dayheader where day_id={day} and header_order={header_id}");
            var reader = dbase.GetReader(cmd);
            reader.Read();
            //Got header ID
            var headerId = reader.GetString("header_id");
            var dayName = reader.GetString("day_name");
            cmd = new MySqlCommand(
                $"select Questions.question, Questions.img_id, Questions.type, Questions.question_id " +
                "from headerquests join Questions on headerquests.question_id=Questions.question_id " +
                $"where header_id={headerId}");
            dbase.Close();
            dbase = new DBManager();
            reader = dbase.GetReader(cmd);
            List<Message> msgList = new List<Message>();
            List<int> questionIds = new List<int>();
            while (reader.Read())
            {
                msgList.Add(new Message
                {
                    type = reader.GetInt32("type"), imageId = reader.GetInt32("img_id"),
                    text = reader.GetString("question")
                });
                questionIds.Add(reader.GetInt32("question_id"));
            }

            //Got all messages
            cmd = new MySqlCommand("select answers.answer_id, answers.answer_text, answers.is_correct, answers.question_id " +
                                   "from headerquests join Questions on headerquests.question_id=Questions.question_id " +
                                   $"join answers on questions.question_id=answers.question_id where header_id={headerId}");
            dbase.Close();
            dbase = new DBManager();
            reader = dbase.GetReader(cmd);
            while (reader.Read())
            {
                for (int i = 0; i < questionIds.Count; i++)
                {
                    if (questionIds[i] == reader.GetInt32("question_id"))
                    {
                        msgList[i].buttons ??= new List<Button>();
                        msgList[i].buttons.Add(new Button
                        {
                            answer_id = reader.GetInt32("answer_id"),
                            is_correct = reader.GetBoolean("is_correct"), text = reader.GetString("answer_text")
                        });
                    }
                }
            }

            var headerMsg = msgList[0];
            msgList.RemoveAt(0);
            return new Header
            {
                buttons = headerMsg.buttons, dayName = dayName, imageId = headerMsg.imageId, messages = msgList,
                text = headerMsg.text
            };
            ;
        }
    }
}