using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace PracticaMerkatorElClimaDropDownList
{
	public partial class _Default : Page
	{
		// Diccionario con provincias de España y algunos municipios de ejemplo
		private static readonly Dictionary<string, List<string>> provinciasEspaña = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
		{
			{ "Madrid", new List<string> { "Madrid", "Alcobendas", "Fuenlabrada", "Leganés", "Getafe", "Móstoles" } },
			{ "Barcelona", new List<string> { "Barcelona", "Badalona", "Terrassa", "Sabadell", "Hospitalet de Llobregat" } },
			{ "Valencia", new List<string> { "Valencia", "Torrent", "Paterna", "Gandía", "Mislata" } },
			{ "Sevilla", new List<string> { "Sevilla", "Dos Hermanas", "Alcalá de Guadaíra", "Utrera" } },
			{ "Málaga", new List<string> { "Málaga", "Marbella", "Estepona", "Torremolinos", "Vélez-Málaga" } }
		};

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack) 
			{
				// Llenar DropDownList con provincias
				ddlCiudades.Items.Clear();
				ddlCiudades.Items.Add(new ListItem("-- Seleccione una provincia --", ""));
				foreach (var provincia in provinciasEspaña.Keys)
				{
					ddlCiudades.Items.Add(new ListItem(provincia, provincia));
				}
			}
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			string ciudad = txtCiudad.Text.Trim();
			string provinciaSeleccionada = ddlCiudades.SelectedValue;

			if (string.IsNullOrEmpty(provinciaSeleccionada))
			{
				lblResultado.Text = "Por favor, seleccione una provincia válida.";
				imgIcono.Visible = false;
				return;
			}

			bool municipioValido = provinciasEspaña.TryGetValue(provinciaSeleccionada, out List<string> municipios) &&
								  municipios.Exists(m => m.Equals(ciudad, StringComparison.OrdinalIgnoreCase));

			if (!municipioValido)
			{
				lblResultado.Text = "La localidad ingresada no corresponde a la provincia seleccionada.";
				imgIcono.Visible = false;
				return;
			}

			string apiKey = "8a166135e27e3fb49a52393ce4e501e0";

			string url = $"https://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid={apiKey}&units=metric&lang=es";

			using (WebClient client = new WebClient())
			{
				try
				{
					string json = client.DownloadString(url);
					JObject datos = JObject.Parse(json);

					string temperatura = datos["main"]["temp"].ToString();
					string clima = datos["weather"][0]["description"].ToString();
					string humedad = datos["main"]["humidity"].ToString();
					string icono = datos["weather"][0]["icon"].ToString();

					lblResultado.Text = $"<strong>Ciudad:</strong> {ciudad}<br />" +
										$"<strong>Temperatura:</strong> {temperatura}°C<br />" +
										$"<strong>Clima:</strong> {clima}<br />" +
										$"<strong>Humedad:</strong> {humedad}%";

					imgIcono.ImageUrl = $"https://openweathermap.org/img/wn/{icono}@2x.png";
					imgIcono.Visible = true;
				}
				catch (WebException)
				{
					lblResultado.Text = "No se pudo obtener la información del clima. Verifique el nombre de la ciudad.";
					imgIcono.Visible = false;
				}
			}
		}

		protected void ddlCiudades_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Opcional: asignar el municipio al TextBox cuando cambia la provincia
			txtCiudad.Text = ddlCiudades.SelectedValue;
		}
	}
}




