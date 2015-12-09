using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text;
using System.Threading;

namespace TestXNA.Objects
{
    public class Item
    {
        private String X_cor;
        private String Y_cor;
        public int type;
        //public Vector2 Position;
        public Item() { }

        public void setX_cor(String cor)
        {
            this.X_cor = cor;
        }
        public void setY_cor(String cor)
        {
            this.Y_cor = cor;
        }
        public int getX_cor()
        {
            return Int32.Parse(X_cor);
        }
        public int getY_cor()
        {
            return Int32.Parse(Y_cor);
        }
        public void settype(int type)
        {
            this.type = type;
        }
        public int gettype()
        {
            return type;
        }
    }
    }

