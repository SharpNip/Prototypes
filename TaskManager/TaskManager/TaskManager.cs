using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskManager
    {


        private static TaskManager mInstance;
        public static TaskManager Instance
        {
            get { return mInstance ?? new TaskManager(); }
        }

        public TaskManager()
        {

        }







    }
}