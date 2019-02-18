using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan
{
    [Serializable]
    class Task
    {
        private string answer;
        private string question;
        private int id;
        private int direction;
        private int x;
        private int y;
        private string answerUser;

        public Task(int ident, string selectAnswer, string selectQuestion, int i, int j)
        {
            id = ident;
            answer = selectAnswer;
            question = selectQuestion;
            direction = 0;
            x = i;
            y = j;
        }

        public void setTaskAnswer(string newAnswerUser)
        {
            answerUser = newAnswerUser;
        }

        public string getTaskAnswer()
        {
            return answerUser;
        }

        public string getAnswer()
        {
            return answer;
        }

        public string getQuestion()
        {
            return question;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setXY(int i, int j)
        {
            x = i;
            y = j;
        }

        public int getID()
        {
            return id;
        }

        public void setDirection(int newDirection)
        {
            direction = newDirection;
        }

        public int getDirection()
        {
            return direction;
        }
    }
}
