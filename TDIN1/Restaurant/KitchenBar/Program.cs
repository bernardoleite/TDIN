using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitchenBar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args[0].Equals("bar"))
                Application.Run(new KitchenBarForm(KitchenBarForm.Type.BAR));
            else if(args[0].Equals("kitchen"))
                Application.Run(new KitchenBarForm(KitchenBarForm.Type.KITCHEN));

        }
    }
}
