using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearViewClinic
{
    class Glasses
    {
        private string glassesId;
        private string glassesBrand;
        private double initialCost;
        private int quantity;
        private string glassesPic;

  

        public Glasses()
        {
          this.glassesId="";
          this.glassesBrand="";
          this.initialCost =0.0;
          this.quantity =0;
          this.glassesPic="";
        }

        public Glasses(string glassesId,string glassesBrand,double initialCost,int quantity,string glassesPic)
        {
            this.glassesId = glassesId;
            this.glassesBrand = glassesBrand;
            this.initialCost = initialCost;
            this.quantity =quantity;
            this.glassesPic = glassesPic;
        }

        public void addGlasses()
        {
            string query = "Insert into glasses (glassId,glassesBrand,initialCost,quantity,glassesPic) values('" + glassesId + "','" + glassesBrand + "','" + initialCost + "','" + quantity + "','" + glassesPic+ "')";
            Crud inserter = new Crud();
            inserter.insertData(query);
        }

        public void updateGlasses(string searchKey)   
        {
    
            string query1 = "Update glasses set glassesBrand='"+glassesBrand+ "',initialCost="+initialCost+",quantity="+quantity+",glassesPic='" +glassesPic + "' where glassId='"+searchKey+"'";
            Crud inserter = new Crud();
            inserter.updateData(query1);
        } 

        public string GlassId
        {
            get { return glassesId; }
            set { glassesId = value; }
        }

        public string GlassesBrand
        {
            get { return glassesBrand; }
            set { glassesBrand = value; }
        }


        public double InitialCost
        {
            get { return initialCost; }
            set { initialCost = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string GlassesPic
        {
            get { return glassesPic; }
            set { glassesPic = value; }
        }
    }
}
