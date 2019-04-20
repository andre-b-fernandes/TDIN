using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;


public class OrderDealingGUI : Form{
    List<Control> controls = new List<Control>();
    private NotPickedOrderListing notPickedOrderListing;
    private InPreparationOrderListing inPrepOrderListing; 

    protected override void OnShown(EventArgs e){
        Console.WriteLine("dasfkdsjfsd");
        foreach (Control item in this.controls){
            item.Select();
            base.OnShown(e);
        }
    }
    
    public OrderDealingGUI(string title){
        BackColor = Color.Silver;
        Text = title;
        Size = new Size(600,400);
        this.notPickedOrderListing = new NotPickedOrderListing(this,  25, 25);
        this.inPrepOrderListing = new InPreparationOrderListing(this, 350, 25);
    }


    public void ButtonClick(object sender, EventArgs e, string type){
        Button button = sender as Button;
        Label label = button.Tag as Label;
        int id = (int) label.Tag;
        if (type.Equals("Not Picked")){
            Console.WriteLine("Not picked -> In prep");
            this.inPrepOrderListing.addOrder(id, this.notPickedOrderListing.Orders[id]);
            this.notPickedOrderListing.removeOrder(id);
        }
        else if(type.Equals("In Preparation")) {
            Console.WriteLine("In prep -> Not picked ");
            this.notPickedOrderListing.addOrder(id, this.inPrepOrderListing.Orders[id]);
            this.inPrepOrderListing.removeOrder(id);
        }
        this.reload();
    }

    private void removeControls(){
        foreach (Control item in this.controls){
            this.Controls.Remove(item);
        }
        this.controls.Clear();
    }

    public void addOrderPartNotPicked(int id, string desc){
        this.notPickedOrderListing.addOrder(id,desc);
        this.reload();
    }

    private void reload(){
        this.removeControls();
        this.notPickedOrderListing.setupComponents(this);
        this.inPrepOrderListing.setupComponents(this);
        Application.DoEvents();
        foreach (Control item in this.controls){
            item.Refresh();
        }
    }
    
    private class NotPickedOrderListing{
        private Dictionary<int,string> orders = new Dictionary<int, string>(); // orderid, description
        private int x,y;
        public Dictionary<int,string> Orders{
            get{
                return orders;
            }
        }
        public NotPickedOrderListing(OrderDealingGUI parent, int startX, int startY){
            this.x = startX;
            this.y = startY;
            // this.addOrder(0,"kslajdas");
            // this.addOrder(1,"sadlçsad");
            this.setupComponents(parent);
        }
        public void setupComponents(OrderDealingGUI parent){
            Label text = new Label();
            text.Text = "Orders Not Picked";
            text.Parent = parent;
            text.BorderStyle = BorderStyle.FixedSingle;
            text.Font = new Font("Arial", 14, FontStyle.Bold);
            text.BackColor = Color.SkyBlue;
            text.Location = new Point(this.x, this.y);
            text.AutoSize = true;
            parent.Controls.Add(text);
            parent.controls.Add(text);
            int i = 0;
            foreach (KeyValuePair<int,string> item in this.orders)
            {
                Label order = new Label();
                order.Tag = item.Key;
                order.Parent = parent;
                order.Text = item.Key + " : " + item.Value;
                order.BorderStyle = BorderStyle.FixedSingle;
                order.Font = new Font("Arial", 14, FontStyle.Bold);
                order.BackColor = Color.SkyBlue;
                order.Location = new Point(this.x, this.y + text.Height*(i+1) + 10);
                order.AutoSize = true;
                parent.Controls.Add(order);
                parent.controls.Add(order);

                Button button = new Button();
                button.Parent = parent;
                button.Text = "->";
                button.BackColor = Color.SkyBlue;
                button.Location = new Point(this.x + order.Width + 10 , this.y + text.Height*(i+1) + 10);
                button.Size = new Size(20,20);
                button.Tag = order;
                button.Click += new EventHandler((sender, e) => parent.ButtonClick(sender, e , "Not Picked") );
                parent.Controls.Add(button);
                parent.controls.Add(button);
                i++;
            } 
        }

        public void addOrder(int orderID, string description){
            if(!this.orders.ContainsKey(orderID))
                this.orders.Add(orderID, description);
        }

        public void removeOrder(int id){
            if(this.orders.ContainsKey(id)){
                Console.WriteLine("Removed order");
                this.orders.Remove(id);
            }
                
        }

    }

       private class InPreparationOrderListing{
        private Dictionary<int,string> orders = new Dictionary<int, string>(); // orderid, description

        private int x,y;

        public Dictionary<int,string> Orders{
            get{
                return orders;
            }
        }
        public InPreparationOrderListing(OrderDealingGUI parent, int startX, int startY){
            this.x = startX;
            this.y = startY;
            this.setupComponents(parent);
        }

        public void setupComponents(OrderDealingGUI parent){
            Label text = new Label();
            text.Text = "Orders In Preparation";
            text.Parent = parent;
            text.BorderStyle = BorderStyle.FixedSingle;
            text.Font = new Font("Arial", 14, FontStyle.Bold);
            text.BackColor = Color.SkyBlue;
            text.Location = new Point(this.x, this.y);
            text.AutoSize = true;
            parent.Controls.Add(text);
            parent.controls.Add(text);
            int i = 0;
            foreach (KeyValuePair<int,string> item in this.orders)
            {
                Label order = new Label();
                order.Tag = item.Key;
                order.Parent = parent;
                order.Text = item.Key + " : " + item.Value;
                order.BorderStyle = BorderStyle.FixedSingle;
                order.Font = new Font("Arial", 14, FontStyle.Bold);
                order.BackColor = Color.SkyBlue;
                order.Location = new Point(this.x, this.y + text.Height*(i+1) + 10);
                order.AutoSize = true;
                parent.Controls.Add(order);
                parent.controls.Add(order);

                Button button = new Button();
                button.Parent = parent;
                button.Text = "<-";
                button.BackColor = Color.SkyBlue;
                button.Location = new Point(this.x + order.Width + 10 , this.y + text.Height*(i+1) + 10);
                button.Size = new Size(20,20);
                button.Tag = order;
                button.Click += new EventHandler((sender, e) => parent.ButtonClick(sender, e , "In Preparation") );
                parent.Controls.Add(button);
                parent.controls.Add(button);

                Button button2 = new Button();
                button2.Parent = parent;
                button2.Text = "->";
                button2.BackColor = Color.SkyBlue;
                button2.Location = new Point(this.x + order.Width + button.Width + 10 , this.y + text.Height*(i+1) + 10);
                button2.Size = new Size(20,20);
                button2.Tag = order;
                //button2.Click += new EventHandler(parent.ButtonClick);
                parent.Controls.Add(button2);
                parent.controls.Add(button2);
                i++;
            } 
        }

        public void addOrder(int orderID, string description){
            if(!this.orders.ContainsKey(orderID))
                this.orders.Add(orderID, description);
        }

        public void removeOrder(int id){
            if(this.orders.ContainsKey(id))
                this.orders.Remove(id);
        }
    }
}