using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXNA.Objects
{
    class Health_pack
    {
        private String X_cor;
        private String Y_cor;
        private int validityper;
        public void setX_cor(String cor)
        {
            this.X_cor = cor;
        }
        public void setY_cor(String cor)
        {
            this.Y_cor = cor;
        }
        public String getX_cor()
        {
            return X_cor;
        }
        public String getY_cor()
        {
            return Y_cor;
        }
        public void setvalidityper(int timeper)
        {
            this.validityper = timeper;
        }
        public int getvalidityper()
        {
            return validityper;
        }
    }
}
