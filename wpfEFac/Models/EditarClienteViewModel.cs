using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class EditarClienteViewModel
    {

        DataClientes data;
        public EditarClienteViewModel()
        {
            data = new DataClientes();
        }

        public Clientes GetCliente(int id)
        {
            Clientes cli = data.getCliente(id);

            if (cli != null)
            {
                return cli;
            }

            return null;
        }

        public Clientes GetRFC(string RFC)
        {
            Clientes cliente = data.getRFC(RFC);

            if (cliente != null)
            {
                return cliente;
            }

            return null;
        }

        public Clientes GetRazonSocial(string Nombre)
        {
            Clientes cliente = data.getRazonSocial(Nombre);

            if (cliente != null)
            {
                return cliente;
            }

            return null;
        }

        public Clientes GetNombreComercial(string Nombre)
        {
            Clientes cliente = data.getNombreComercial(Nombre);

            if (cliente != null)
            {
                return cliente;
            }

            return null;
        }

        public Clientes GetGiro(string Giro)
        {
            Clientes cliente = data.getGiro(Giro);

            if (cliente != null)
            {
                return cliente;
            }

            return null;
        }

        public Clientes GetTipoInscripcion(string Tipo)
        {
            Clientes cliente = data.getTipoInscripcion(Tipo);

            if (cliente != null)
            {
                return cliente;
            }

            return null;
        }

        public Direcciones_Fiscales GetCalle(int idCliente)
        {
            Direcciones_Fiscales cliente = data.getCalle(idCliente);

            if (cliente != null)
            {
                return cliente;
            }

            return null;
        }

        public ObservableCollection<Estado> GetEstados()
        {
            return data.getEstado();
        }
    }
}
