using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan
{
    [Serializable]
    class Scan
    {
        private List<Task> tasks;
        private int height;
        private int width;

        public Scan(List<Task> currentTasks, int h, int w)
        {
            tasks = currentTasks;
            height = h;
            width = w;
        }

        public List<Task> getTasks()
        {
            return tasks;
        }

        public void addTask(Task newTask)
        {
            tasks.Add(newTask);
        }

        public Task getTask(int ident)
        {
            Task result = null;
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].getID() == ident)
                {
                    result = tasks[i];
                }
            }
            return result;
        }

        public void deleteTask(int ident)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].getID() == ident)
                {
                    tasks.Remove(tasks[i]);
                }
            }
        }

        public int getHeight()
        {
            return height;
        }

        public int getWidth()
        {
            return width;
        }
    }
}
