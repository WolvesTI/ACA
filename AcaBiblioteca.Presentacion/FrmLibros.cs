using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using AcaBiblioteca.Datos;
using System.Linq.Expressions;

namespace AcaBiblioteca
{
    public partial class FrmLibros : Form
    {

        private bool modoEdicion = false;
        public FrmLibros()
        {
            InitializeComponent();
            CargarLibros();
            CargarAutores();
            CargarEditoriales();
        }

        private void CargarLibros()
        {
            try
            {
                Conexion conexion = new Conexion();

                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    string consulta = "SELECT * FROM Libros";

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, cn))
                    {
                        DataTable tabla = new DataTable();

                        adaptador.Fill(tabla);

                        dgvLibros.DataSource = tabla;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los libros:\n" + ex.Message,
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarAutores()
        {
            try
            {
                Conexion conexion = new Conexion();
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    string consulta = "SELECT IdAutor, Nombre, Apellido FROM Autores ORDER BY Nombre, Apellido";
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, cn))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        tabla.Columns.Add("NombreCompleto", typeof(string));
                        foreach (DataRow fila in tabla.Rows)
                        {
                            fila["NombreCompleto"] = fila["Nombre"].ToString() + " " + fila["Apellido"].ToString();
                        }
                        cmbAutor.DataSource = tabla;
                        cmbAutor.DisplayMember = "NombreCompleto";
                        cmbAutor.ValueMember = "IdAutor";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los autores:\n" + ex.Message,
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void CargarEditoriales()
        {
            try
            {
                Conexion conexion = new Conexion();

                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    string consulta = "SELECT IdEditorial, Nombre FROM Editoriales ORDER BY Nombre";

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, cn))
                    {
                        DataTable tabla = new DataTable();

                        adaptador.Fill(tabla);

                        cmbEditorial.DataSource = tabla;
                        cmbEditorial.DisplayMember = "Nombre";
                        cmbEditorial.ValueMember = "IdEditorial";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las editoriales:\n" + ex.Message,
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtBuscarISBN.Text))
                {
                    MessageBox.Show(
                        "Por favor, ingrese un ISBN para buscar.",
                        "Biblioteca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                Conexion conexion = new Conexion();

                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    string consulta = "SELECT * FROM Libros WHERE ISBN = @ISBN";
                    using (SqlCommand comando = new SqlCommand(consulta, cn))
                    {
                        comando.Parameters.AddWithValue("@ISBN", txtBuscarISBN.Text.Trim());
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                txtISBN.Text = lector["ISBN"].ToString();
                                txtTitulo.Text = lector["Titulo"].ToString();
                                cmbAutor.SelectedValue = Convert.ToInt32(lector["IdAutor"]);
                                cmbEditorial.SelectedValue = Convert.ToInt32(lector["IdEditorial"]);
                                txtCategoria.Text = lector["Categoria"].ToString();
                                txtAnio.Text = lector["Anio"].ToString();
                                txtExistencias.Text = lector["Existencias"].ToString();

                                MessageBox.Show(
                                    "Libro encontrado y cargado en los campos de texto.",
                                    "Biblioteca",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show(
                                    "No se encontró ningún libro con el ISBN proporcionado.",
                                    "Biblioteca",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar el libro:\n" + ex.Message,
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            txtISBN.Clear();
            txtTitulo.Clear();
            cmbAutor.SelectedIndex = -1;
            cmbEditorial.SelectedIndex = -1;
            txtCategoria.Clear();
            txtAnio.Clear();
            txtExistencias.Clear();
            txtISBN.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtISBN.Text) ||
                    string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                    cmbAutor.SelectedIndex == -1 ||
                    cmbEditorial.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                    string.IsNullOrWhiteSpace(txtAnio.Text) ||
                    string.IsNullOrWhiteSpace(txtExistencias.Text))
                {
                    MessageBox.Show(
                        "Complete todos los campos antes de guardar.",
                        "Biblioteca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                int anio;
                int existencias;

                if (!int.TryParse(txtAnio.Text, out anio))
                {
                    MessageBox.Show(
                        "El año debe ser un número entero.",
                        "Biblioteca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (!int.TryParse(txtExistencias.Text, out existencias))
                {
                    MessageBox.Show(
                        "Las existencias deben ser un número entero.",
                        "Biblioteca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                Conexion conexion = new Conexion();

                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    string consulta = @"INSERT INTO Libros
                                (ISBN, Titulo, IdAutor, IdEditorial, Categoria, Anio, Existencias)
                                VALUES
                                (@ISBN, @Titulo, @IdAutor, @IdEditorial, @Categoria, @Anio, @Existencias)";

                    using (SqlCommand comando = new SqlCommand(consulta, cn))
                    {
                        comando.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                        comando.Parameters.AddWithValue("@Titulo", txtTitulo.Text.Trim());
                        comando.Parameters.AddWithValue("@IdAutor", Convert.ToInt32(cmbAutor.SelectedValue));
                        comando.Parameters.AddWithValue("@IdEditorial", Convert.ToInt32(cmbEditorial.SelectedValue));
                        comando.Parameters.AddWithValue("@Categoria", txtCategoria.Text.Trim());
                        comando.Parameters.AddWithValue("@Anio", anio);
                        comando.Parameters.AddWithValue("@Existencias", existencias);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Libro guardado correctamente.",
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarLibros();

                btnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar el libro:\n" + ex.Message,
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvLibros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            txtISBN.Text = dgvLibros.Rows[e.RowIndex].Cells["ISBN"].Value.ToString();
            txtTitulo.Text = dgvLibros.Rows[e.RowIndex].Cells["Titulo"].Value.ToString();

            cmbAutor.SelectedValue = Convert.ToInt32(
                dgvLibros.Rows[e.RowIndex].Cells["IdAutor"].Value
            );

            cmbEditorial.SelectedValue = Convert.ToInt32(
                dgvLibros.Rows[e.RowIndex].Cells["IdEditorial"].Value
            );

            txtCategoria.Text = dgvLibros.Rows[e.RowIndex].Cells["Categoria"].Value.ToString();
            txtAnio.Text = dgvLibros.Rows[e.RowIndex].Cells["Anio"].Value.ToString();
            txtExistencias.Text = dgvLibros.Rows[e.RowIndex].Cells["Existencias"].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtISBN.Text) ||
                    string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                    cmbAutor.SelectedIndex == -1 ||
                    cmbEditorial.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                    string.IsNullOrWhiteSpace(txtAnio.Text) ||
                    string.IsNullOrWhiteSpace(txtExistencias.Text))
                {
                    MessageBox.Show(
                        "Complete todos los campos antes de editar.",
                        "Biblioteca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                int anio;
                int existencias;

                if (!int.TryParse(txtAnio.Text, out anio))
                {
                    MessageBox.Show(
                        "El año debe ser un número entero.",
                        "Biblioteca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (!int.TryParse(txtExistencias.Text, out existencias))
                {
                    MessageBox.Show(
                        "Las existencias deben ser un número entero.",
                        "Biblioteca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                Conexion conexion = new Conexion();

                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    string consulta = @"UPDATE Libros
                                SET Titulo = @Titulo,
                                    IdAutor = @IdAutor,
                                    IdEditorial = @IdEditorial,
                                    Categoria = @Categoria,
                                    Anio = @Anio,
                                    Existencias = @Existencias
                                WHERE ISBN = @ISBN";

                    using (SqlCommand comando = new SqlCommand(consulta, cn))
                    {
                        comando.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                        comando.Parameters.AddWithValue("@Titulo", txtTitulo.Text.Trim());
                        comando.Parameters.AddWithValue("@IdAutor", Convert.ToInt32(cmbAutor.SelectedValue));
                        comando.Parameters.AddWithValue("@IdEditorial", Convert.ToInt32(cmbEditorial.SelectedValue));
                        comando.Parameters.AddWithValue("@Categoria", txtCategoria.Text.Trim());
                        comando.Parameters.AddWithValue("@Anio", anio);
                        comando.Parameters.AddWithValue("@Existencias", existencias);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Libro actualizado correctamente.",
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarLibros();
                btnCancelar_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al actualizar el libro:\n" + ex.Message,
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtISBN.Clear();
            txtTitulo.Clear();
            txtCategoria.Clear();
            txtAnio.Clear();
            txtExistencias.Clear();

            cmbAutor.SelectedIndex = -1;
            cmbEditorial.SelectedIndex = -1;

            modoEdicion = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLibros.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Seleccione un libro para eliminar.",
                        "Biblioteca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string isbn = dgvLibros.CurrentRow.Cells["ISBN"].Value.ToString();

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de eliminar este libro?",
                    "Biblioteca",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    return;
                }

                Conexion conexion = new Conexion();

                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    string consulta = "DELETE FROM Libros WHERE ISBN = @ISBN";

                    using (SqlCommand comando = new SqlCommand(consulta, cn))
                    {
                        comando.Parameters.AddWithValue("@ISBN", isbn);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Libro eliminado correctamente.",
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                modoEdicion = false;
                txtISBN.Enabled = true;

                CargarLibros();

                btnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar el libro:\n" + ex.Message,
                    "Biblioteca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
