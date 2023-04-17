//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace WPFProjekt.UserControls
//{
//    / <summary>
//    / Logika interakcji dla klasy UserControl1.xaml
//    / </summary>
//    public partial class UserControl1 : UserControl
//    {
//        public partial class MyUserControl : UserControl
//        {
//            private Klient klient;

//            public MyUserControl()
//            {

//            }


//            public static readonly DependencyProperty MyPropertyProperty =
//                DependencyProperty.Register("MyProperty", typeof(string), typeof(MyUserControl), new PropertyMetadata(""));

//            public string MyProperty
//            {
//                get { return (string)GetValue(MyPropertyProperty); }
//                set { SetValue(MyPropertyProperty, value); }
//            }

//            public void SetDataContext(int entityId)
//            {
//                using (var context = new Wypozyczalnia2Entities())
//                {
//                    klient = context.Klient.Find(entityId);
//                    DataContext = klient;
//                }
//            }
//        }
//    }
//}
