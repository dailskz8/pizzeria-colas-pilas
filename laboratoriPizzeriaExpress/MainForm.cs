using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace laboratoriPizzeriaCampusExpress
{
    public partial class MainForm : Form
    {
        // Colecciones principales: FIFO para pedidos, LIFO para bitácora
        private Queue<string> pedidosPremium = new Queue<string>();
        private Stack<string> bitacoraP = new Stack<string>();
       	private Queue<string> colaPedidos = new Queue<string>();
        private Stack<string> pilaBitacora = new Stack<string>();

        public MainForm()
        {
            InitializeComponent();
            ActualizarUI();
        }

        // PASO 1: Nuevo pedido (FIFO entrada)
        private void BtnNuevoPedido_Click(object sender, EventArgs e)
        {
            
        	string cliente = txtCliente.Text.Trim();

        	DialogResult respuesta = MessageBox.Show("El cliente es Premium?","Confirmacion", MessageBoxButtons.YesNo);
        	
            // Validar entrada
            if (cliente == "")
            {
                lblEstado.Text = string.Format("⚠️ Debe ingresar un nombre de cliente.");
                return;
            }
            
            if ( respuesta == DialogResult.Yes) {
            	
            	pedidosPremium.Enqueue(cliente);
            	
            	bitacoraP.Push(string.Format("PEDIDO: {0}", cliente));
            	
            	 txtCliente.Clear();
            	lblEstado.Text = string.Format("✅ Pedido registrado para {0}", cliente);
           		 ActualizarUI();
            	
            		
            } else {
            
            // Agregar a la cola
            colaPedidos.Enqueue(cliente);

            // Registrar en la pila
            pilaBitacora.Push(string.Format("PEDIDO: {0}", cliente));

            // Limpiar campo y actualizar
            txtCliente.Clear();
            lblEstado.Text = string.Format("✅ Pedido registrado para {0}", cliente);
            ActualizarUI();
        	}
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (pedidosPremium.Count > 0)
 	   {
    	    string cliente = pedidosPremium.Dequeue();
    	    bitacoraP.Push(string.Format("ENTREGADO: {0}", cliente));
    	    lblEstado.Text = string.Format("⭐ Pedido Premium entregado a {0}", cliente);
    	}
   	
    	else if (colaPedidos.Count > 0)
    	{
    	    string cliente = colaPedidos.Dequeue();
    	    pilaBitacora.Push(string.Format("ENTREGADO: {0}", cliente));
    	    lblEstado.Text = string.Format("🍕 Pedido entregado a {0}", cliente);
    	}

    	else
    	{
    	    lblEstado.Text = string.Format("❌ No hay pedidos pendientes.");
    	}

    	ActualizarUI();
        
      
    	}
	
    	    private void BtnDeshacer_Click(object sender, EventArgs e)
    	    {
    	       
    	
    		if (pilaBitacora.Count == 0 && bitacoraP.Count == 0)
    	{
    	    lblEstado.Text = string.Format("📭 No hay acciones para deshacer.");
    	    return;
    	}

    
    		bool deshacerPremium = false;
	
    		if (bitacoraP.Count > 0 && pilaBitacora.Count > 0)
    	{
    	    DialogResult respuesta = MessageBox.Show("¿Desea deshacer la última acción Premium?\n(Seleccione 'No' para deshacer la normal)", "Deshacer", MessageBoxButtons.YesNo);
    	    deshacerPremium = (respuesta == DialogResult.Yes);
    	}
    		else if (bitacoraP.Count > 0)
    	{
    	    deshacerPremium = true; 
    	}

    		if (deshacerPremium)
    	{
    	    string ultimaAccion = bitacoraP.Pop();
	
    	    if (ultimaAccion.StartsWith("PEDIDO:"))
    	    {
            string nombre = ultimaAccion.Replace("PEDIDO: ", "").Trim();
    	        string[] temporal = pedidosPremium.ToArray();
    	        pedidosPremium.Clear();
    	        foreach (string p in temporal)
    	        {
    	            if (p != nombre) pedidosPremium.Enqueue(p);
    	        }
    	        lblEstado.Text = string.Format("↩️ Se deshizo el pedido Premium de {0}", nombre);
    	    }
    	    else if (ultimaAccion.StartsWith("ENTREGADO:"))
    	    {
    	        string nombre = ultimaAccion.Replace("ENTREGADO: ", "").Trim();
    	        pedidosPremium.Enqueue(nombre);
    	        lblEstado.Text = string.Format("↩️ Se deshizo la entrega Premium a {0}", nombre);
    		    }
    	}
    	else
    	{
    	    string ultimaAccion = pilaBitacora.Pop();
	
    	    if (ultimaAccion.StartsWith("PEDIDO:"))
    		    {
    	        string nombre = ultimaAccion.Replace("PEDIDO: ", "").Trim();
    	        string[] temporal = colaPedidos.ToArray();
    	        colaPedidos.Clear();
    	        foreach (string p in temporal)
    	        {
    	            if (p != nombre) colaPedidos.Enqueue(p);
    	        }
    	        lblEstado.Text = string.Format("↩️ Se deshizo el pedido Normal de {0}", nombre);
    	    }
    	    else if (ultimaAccion.StartsWith("ENTREGADO:"))
    	    {
    	        string nombre = ultimaAccion.Replace("ENTREGADO: ", "").Trim();
    	        colaPedidos.Enqueue(nombre);
   	         lblEstado.Text = string.Format("↩️ Se deshizo la entrega Normal a {0}", nombre);
    	    }
  	  }

   		 ActualizarUI();
        }
    	    

        // PASO 4: Limpiar todo (reiniciar sistema)
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            colaPedidos.Clear();
            pilaBitacora.Clear();
            pedidosPremium.Clear();
            bitacoraP.Clear();
            lblEstado.Text = string.Format("🧹 Sistema reiniciado.");
            ActualizarUI();
        }

        // Sincronizar la interfaz con el estado actual
        private void ActualizarUI()
        {
            // Limpiar listas visuales
            lstPedidos.Items.Clear();
            lstBitacora.Items.Clear();
            lstPedidosP.Items.Clear();
            lstBitacoraP.Items.Clear();

            // Mostrar cola de pedidos
            foreach (string p in colaPedidos)
                lstPedidos.Items.Add(p);
            if (colaPedidos.Count == 0)
                lstPedidos.Items.Add("(Sin pedidos pendientes)");
            
            foreach (string p in pedidosPremium)
                lstPedidosP.Items.Add(p);
            if (pedidosPremium.Count == 0)
                lstPedidosP.Items.Add("(Sin pedidos pendientes)");

            // Mostrar bitácora (pila)
            foreach (string accion in pilaBitacora)
                lstBitacora.Items.Add(accion);
            if (pilaBitacora.Count == 0)
                lstBitacora.Items.Add("(Sin acciones registradas)");
            
            foreach (string accion in bitacoraP)
                lstBitacoraP.Items.Add(accion);
            if (bitacoraP.Count == 0)
                lstBitacoraP.Items.Add("(Sin acciones registradas)");

            // Actualizar contador
            lblContador.Text = string.Format("Pedidos: {0} | Pedidos Premium: {1}| Bitacora Premium: {2} |Bitácora: {3}",
                colaPedidos.Count, pedidosPremium.Count, bitacoraP.Count, pilaBitacora.Count);
        }
       
        
    }
}