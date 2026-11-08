
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

// (CapaEntidades)

namespace CapaEntidades
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }

    public class Editorial
    {
        public int IdEditorial { get; set; }
        public string Nombre { get; set; }
    }

    public class Libro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public int IdAutor { get; set; }
        public int IdEditorial { get; set; }
        public string Categoria { get; set; }
        public int Anio { get; set; }
        public int Existencias { get; set; }

        // Propiedades auxiliares para despliegue en UI
        public string NombreAutor { get; set; }
        public string NombreEditorial { get; set; }
    }

    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }

    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public int IdUsuario { get; set; }
        public string ISBN { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string Estado { get; set; }

        // Propiedades auxiliares para UI
        public string NombreUsuario { get; set; }
        public string TituloLibro { get; set; }
    }
}


// (CapaDatos)

namespace CapaDatos
{
    using CapaEntidades;

    public class ConexionBD
    {
        private readonly string cadenaConexion = @"Server=localhost;Database=AcaBiblioteca;Integrated Security=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }

    public class AutorDAL
    {
        private readonly ConexionBD conexion = new ConexionBD();

        public List<Autor> ListarAutores()
        {
            List<Autor> lista = new List<Autor>();
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = "SELECT IdAutor, Nombre, Apellido FROM Autores ORDER BY Apellido, Nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Autor
                        {
                            IdAutor = Convert.ToInt32(dr["IdAutor"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public bool InsertarAutor(Autor a)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = "INSERT INTO Autores (Nombre, Apellido) VALUES (@Nombre, @Apellido)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", a.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", a.Apellido);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public class EditorialDAL
    {
        private readonly ConexionBD conexion = new ConexionBD();

        public List<Editorial> ListarEditoriales()
        {
            List<Editorial> lista = new List<Editorial>();
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = "SELECT IdEditorial, Nombre FROM Editoriales ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Editorial
                        {
                            IdEditorial = Convert.ToInt32(dr["IdEditorial"]),
                            Nombre = dr["Nombre"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public bool InsertarEditorial(Editorial e)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = "INSERT INTO Editoriales (Nombre) VALUES (@Nombre)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public class LibroDAL
    {
        private readonly ConexionBD conexion = new ConexionBD();

        public List<Libro> ListarLibros()
        {
            List<Libro> lista = new List<Libro>();
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = @"SELECT L.ISBN, L.Titulo, L.IdAutor, L.IdEditorial, L.Categoria, L.Anio, L.Existencias,
                                        A.Nombre + ' ' + A.Apellido AS Autor, E.Nombre AS Editorial
                                 FROM Libros L
                                 INNER JOIN Autores A ON L.IdAutor = A.IdAutor
                                 INNER JOIN Editoriales E ON L.IdEditorial = E.IdEditorial
                                 ORDER BY L.Titulo";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Libro
                        {
                            ISBN = dr["ISBN"].ToString(),
                            Titulo = dr["Titulo"].ToString(),
                            IdAutor = Convert.ToInt32(dr["IdAutor"]),
                            IdEditorial = Convert.ToInt32(dr["IdEditorial"]),
                            Categoria = dr["Categoria"].ToString(),
                            Anio = Convert.ToInt32(dr["Anio"]),
                            Existencias = Convert.ToInt32(dr["Existencias"]),
                            NombreAutor = dr["Autor"].ToString(),
                            NombreEditorial = dr["Editorial"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public bool InsertarLibro(Libro l)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Libros (ISBN, Titulo, IdAutor, IdEditorial, Categoria, Anio, Existencias)
                                 VALUES (@ISBN, @Titulo, @IdAutor, @IdEditorial, @Categoria, @Anio, @Existencias)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ISBN", l.ISBN);
                cmd.Parameters.AddWithValue("@Titulo", l.Titulo);
                cmd.Parameters.AddWithValue("@IdAutor", l.IdAutor);
                cmd.Parameters.AddWithValue("@IdEditorial", l.IdEditorial);
                cmd.Parameters.AddWithValue("@Categoria", l.Categoria);
                cmd.Parameters.AddWithValue("@Anio", l.Anio);
                cmd.Parameters.AddWithValue("@Existencias", l.Existencias);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ActualizarExistencias(string isbn, int cambio)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = "UPDATE Libros SET Existencias = Existencias + @Cambio WHERE ISBN = @ISBN";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Cambio", cambio);
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public class UsuarioDAL
    {
        private readonly ConexionBD conexion = new ConexionBD();

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = "SELECT IdUsuario, Nombre, Apellido, Documento, Telefono, Correo FROM Usuarios ORDER BY Apellido, Nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            Documento = dr["Documento"].ToString(),
                            Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : "",
                            Correo = dr["Correo"] != DBNull.Value ? dr["Correo"].ToString() : ""
                        });
                    }
                }
            }
            return lista;
        }

        public bool InsertarUsuario(Usuario u)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Usuarios (Nombre, Apellido, Documento, Telefono, Correo)
                                 VALUES (@Nombre, @Apellido, @Documento, @Telefono, @Correo)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", u.Apellido);
                cmd.Parameters.AddWithValue("@Documento", u.Documento);
                cmd.Parameters.AddWithValue("@Telefono", u.Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", u.Correo ?? (object)DBNull.Value);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public class PrestamoDAL
    {
        private readonly ConexionBD conexion = new ConexionBD();

        public List<Prestamo> ListarPrestamos()
        {
            List<Prestamo> lista = new List<Prestamo>();
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = @"SELECT P.IdPrestamo, P.IdUsuario, P.ISBN, P.FechaPrestamo, P.FechaDevolucion, P.Estado,
                                        U.Nombre + ' ' + U.Apellido AS Usuario, L.Titulo AS Libro
                                 FROM Prestamos P
                                 INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario
                                 INNER JOIN Libros L ON P.ISBN = L.ISBN
                                 ORDER BY P.IdPrestamo DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Prestamo
                        {
                            IdPrestamo = Convert.ToInt32(dr["IdPrestamo"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            ISBN = dr["ISBN"].ToString(),
                            FechaPrestamo = Convert.ToDateTime(dr["FechaPrestamo"]),
                            FechaDevolucion = dr["FechaDevolucion"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["FechaDevolucion"]) : null,
                            Estado = dr["Estado"].ToString(),
                            NombreUsuario = dr["Usuario"].ToString(),
                            TituloLibro = dr["Libro"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public bool RegistrarPrestamo(int idUsuario, string isbn)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    string insertQuery = @"INSERT INTO Prestamos (IdUsuario, ISBN, FechaPrestamo, FechaDevolucion, Estado)
                                           VALUES (@IdUsuario, @ISBN, GETDATE(), NULL, 'Prestado')";
                    SqlCommand cmdInsert = new SqlCommand(insertQuery, con, tx);
                    cmdInsert.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmdInsert.Parameters.AddWithValue("@ISBN", isbn);
                    cmdInsert.ExecuteNonQuery();

                    string updateQuery = "UPDATE Libros SET Existencias = Existencias - 1 WHERE ISBN = @ISBN AND Existencias > 0";
                    SqlCommand cmdUpdate = new SqlCommand(updateQuery, con, tx);
                    cmdUpdate.Parameters.AddWithValue("@ISBN", isbn);
                    int filas = cmdUpdate.ExecuteNonQuery();

                    if (filas == 0)
                    {
                        tx.Rollback();
                        return false;
                    }

                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public bool RegistrarDevolucion(int idPrestamo, string isbn)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    string updatePrestamo = @"UPDATE Prestamos 
                                              SET FechaDevolucion = GETDATE(), Estado = 'Devuelto' 
                                              WHERE IdPrestamo = @IdPrestamo AND Estado = 'Prestado'";
                    SqlCommand cmdP = new SqlCommand(updatePrestamo, con, tx);
                    cmdP.Parameters.AddWithValue("@IdPrestamo", idPrestamo);
                    int filas = cmdP.ExecuteNonQuery();

                    if (filas == 0)
                    {
                        tx.Rollback();
                        return false;
                    }

                    string updateLibro = "UPDATE Libros SET Existencias = Existencias + 1 WHERE ISBN = @ISBN";
                    SqlCommand cmdL = new SqlCommand(updateLibro, con, tx);
                    cmdL.Parameters.AddWithValue("@ISBN", isbn);
                    cmdL.ExecuteNonQuery();

                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}


//  (CapaNegocio)

namespace CapaNegocio
{
    using CapaDatos;
    using CapaEntidades;

    public class BibliotecaBLL
    {
        private readonly AutorDAL autorDAL = new AutorDAL();
        private readonly EditorialDAL editorialDAL = new EditorialDAL();
        private readonly LibroDAL libroDAL = new LibroDAL();
        private readonly UsuarioDAL usuarioDAL = new UsuarioDAL();
        private readonly PrestamoDAL prestamoDAL = new PrestamoDAL();

        // Autores
        public List<Autor> ObtenerAutores() => autorDAL.ListarAutores();
        public bool GuardarAutor(string nombre, string apellido)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El nombre y apellido son obligatorios.");
            return autorDAL.InsertarAutor(new Autor { Nombre = nombre.Trim(), Apellido = apellido.Trim() });
        }

        // Editoriales
        public List<Editorial> ObtenerEditoriales() => editorialDAL.ListarEditoriales();
        public bool GuardarEditorial(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la editorial es obligatorio.");
            return editorialDAL.InsertarEditorial(new Editorial { Nombre = nombre.Trim() });
        }

        // Libros
        public List<Libro> ObtenerLibros() => libroDAL.ListarLibros();
        public bool GuardarLibro(string isbn, string titulo, int idAutor, int idEditorial, string categoria, int anio, int existencias)
        {
            if (string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El ISBN y el título son obligatorios.");
            if (existencias < 0)
                throw new ArgumentException("Las existencias no pueden ser negativas.");

            return libroDAL.InsertarLibro(new Libro
            {
                ISBN = isbn.Trim(),
                Titulo = titulo.Trim(),
                IdAutor = idAutor,
                IdEditorial = idEditorial,
                Categoria = categoria?.Trim(),
                Anio = anio,
                Existencias = existencias
            });
        }

        // Usuarios
        public List<Usuario> ObtenerUsuarios() => usuarioDAL.ListarUsuarios();
        public bool GuardarUsuario(string nombre, string apellido, string documento, string telefono, string correo)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(documento))
                throw new ArgumentException("Nombre, apellido y documento son obligatorios.");
            return usuarioDAL.InsertarUsuario(new Usuario
            {
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                Documento = documento.Trim(),
                Telefono = telefono?.Trim(),
                Correo = correo?.Trim()
            });
        }

        // Prestamos
        public List<Prestamo> ObtenerPrestamos() => prestamoDAL.ListarPrestamos();
        public bool PrestarLibro(int idUsuario, string isbn)
        {
            if (idUsuario <= 0 || string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("Debe seleccionar un usuario y un libro válido.");
            return prestamoDAL.RegistrarPrestamo(idUsuario, isbn);
        }

        public bool DevolverLibro(int idPrestamo, string isbn)
        {
            if (idPrestamo <= 0 || string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("Datos del préstamo no válidos.");
            return prestamoDAL.RegistrarDevolucion(idPrestamo, isbn);
        }
    }
}


// (CapaPresentacion - Windows Forms)

namespace CapaPresentacion
{
    using CapaEntidades;
    using CapaNegocio;

    // ---  (MDI / MENÚ) ---
    public class FormPrincipal : Form
    {
        private MenuStrip menuStrip;
        private ToolStripMenuItem menuGestion;
        private ToolStripMenuItem itemLibros;
        private ToolStripMenuItem itemUsuarios;
        private ToolStripMenuItem itemPrestamos;
        private ToolStripMenuItem itemSalir;

        public FormPrincipal()
        {
            this.Title = "Sistema de Gestión de Biblioteca - ACA Programación Avanzada";
            this.Text = "Sistema de Gestión de Biblioteca";
            this.Width = 1024;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;

            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            menuStrip = new MenuStrip();
            menuGestion = new ToolStripMenuItem("Gestión de Biblioteca");
            itemLibros = new ToolStripMenuItem("Gestión de Libros", null, (s, e) => AbrirFormulario(new FormLibros()));
            itemUsuarios = new ToolStripMenuItem("Gestión de Usuarios", null, (s, e) => AbrirFormulario(new FormUsuarios()));
            itemPrestamos = new ToolStripMenuItem("Préstamos y Devoluciones", null, (s, e) => AbrirFormulario(new FormPrestamos()));
            itemSalir = new ToolStripMenuItem("Salir", null, (s, e) => Application.Exit());

            menuGestion.DropDownItems.Add(itemLibros);
            menuGestion.DropDownItems.Add(itemUsuarios);
            menuGestion.DropDownItems.Add(itemPrestamos);
            menuGestion.DropDownItems.Add(new ToolStripSeparator());
            menuGestion.DropDownItems.Add(itemSalir);

            menuStrip.Items.Add(menuGestion);
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void AbrirFormulario(Form formHijo)
        {
            formHijo.MdiParent = this;
            formHijo.Show();
        }
    }

    // GESTIÓN DE LIBROS 
    public class FormLibros : Form
    {
        private BibliotecaBLL bll = new BibliotecaBLL();
        private DataGridView dgvLibros;
        private TextBox txtISBN, txtTitulo, txtCategoria, txtAnio, txtExistencias;
        private ComboBox cbAutor, cbEditorial;
        private Button btnGuardar, btnRefrescar;

        public FormLibros()
        {
            this.Text = "Gestión de Libros (CRUD)";
            this.Size = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            BuildUI();
            CargarCombos();
            CargarGrid();
        }

        private void BuildUI()
        {
            Panel panelForm = new Panel { Dock = DockStyle.Top, Height = 180, Padding = new Padding(10) };
            
            Label lblISBN = new Label { Text = "ISBN:", Location = new Point(20, 20), AutoSize = true };
            txtISBN = new TextBox { Location = new Point(100, 17), Width = 150 };

            Label lblTitulo = new Label { Text = "Título:", Location = new Point(280, 20), AutoSize = true };
            txtTitulo = new TextBox { Location = new Point(350, 17), Width = 450 };

            Label lblAutor = new Label { Text = "Autor:", Location = new Point(20, 55), AutoSize = true };
            cbAutor = new ComboBox { Location = new Point(100, 52), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblEditorial = new Label { Text = "Editorial:", Location = new Point(380, 55), AutoSize = true };
            cbEditorial = new ComboBox { Location = new Point(460, 52), Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblCat = new Label { Text = "Categoría:", Location = new Point(20, 90), AutoSize = true };
            txtCategoria = new TextBox { Location = new Point(100, 87), Width = 180 };

            Label lblAnio = new Label { Text = "Año:", Location = new Point(300, 90), AutoSize = true };
            txtAnio = new TextBox { Location = new Point(350, 87), Width = 80 };

            Label lblEx = new Label { Text = "Existencias:", Location = new Point(460, 90), AutoSize = true };
            txtExistencias = new TextBox { Location = new Point(550, 87), Width = 80 };

            btnGuardar = new Button { Text = "Guardar Libro", Location = new Point(650, 85), Width = 150, Height = 30, BackColor = Color.LightSkyBlue };
            btnGuardar.Click += BtnGuardar_Click;

            panelForm.Controls.AddRange(new Control[] {
                lblISBN, txtISBN, lblTitulo, txtTitulo,
                lblAutor, cbAutor, lblEditorial, cbEditorial,
                lblCat, txtCategoria, lblAnio, txtAnio, lblEx, txtExistencias,
                btnGuardar
            });

            dgvLibros = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true };

            this.Controls.Add(dgvLibros);
            this.Controls.Add(panelForm);
        }

        private void CargarCombos()
        {
            cbAutor.DataSource = bll.ObtenerAutores();
            cbAutor.DisplayMember = "NombreCompleto";
            cbAutor.ValueMember = "IdAutor";

            cbEditorial.DataSource = bll.ObtenerEditoriales();
            cbEditorial.DisplayMember = "Nombre";
            cbEditorial.ValueMember = "IdEditorial";
        }

        private void CargarGrid()
        {
            dgvLibros.DataSource = bll.ObtenerLibros();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idAutor = (int)cbAutor.SelectedValue;
                int idEditorial = (int)cbEditorial.SelectedValue;
                int anio = int.Parse(txtAnio.Text);
                int existencias = int.Parse(txtExistencias.Text);

                bool ok = bll.GuardarLibro(txtISBN.Text, txtTitulo.Text, idAutor, idEditorial, txtCategoria.Text, anio, existencias);
                if (ok)
                {
                    MessageBox.Show("Libro guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrid();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            txtISBN.Clear();
            txtTitulo.Clear();
            txtCategoria.Clear();
            txtAnio.Clear();
            txtExistencias.Clear();
        }
    }

    //  GESTIÓN DE USUARIOS 
    public class FormUsuarios : Form
    {
        private BibliotecaBLL bll = new BibliotecaBLL();
        private DataGridView dgvUsuarios;
        private TextBox txtNombre, txtApellido, txtDocumento, txtTelefono, txtCorreo;
        private Button btnGuardar;

        public FormUsuarios()
        {
            this.Text = "Gestión de Usuarios (CRUD)";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            BuildUI();
            CargarGrid();
        }

        private void BuildUI()
        {
            Panel panelForm = new Panel { Dock = DockStyle.Top, Height = 140, Padding = new Padding(10) };

            Label lblDoc = new Label { Text = "Documento:", Location = new Point(20, 20), AutoSize = true };
            txtDocumento = new TextBox { Location = new Point(100, 17), Width = 150 };

            Label lblNom = new Label { Text = "Nombre:", Location = new Point(270, 20), AutoSize = true };
            txtNombre = new TextBox { Location = new Point(340, 17), Width = 180 };

            Label lblApe = new Label { Text = "Apellido:", Location = new Point(530, 20), AutoSize = true };
            txtApellido = new TextBox { Location = new Point(600, 17), Width = 150 };

            Label lblTel = new Label { Text = "Teléfono:", Location = new Point(20, 60), AutoSize = true };
            txtTelefono = new TextBox { Location = new Point(100, 57), Width = 150 };

            Label lblCorreo = new Label { Text = "Correo:", Location = new Point(270, 60), AutoSize = true };
            txtCorreo = new TextBox { Location = new Point(340, 57), Width = 250 };

            btnGuardar = new Button { Text = "Registrar Usuario", Location = new Point(600, 55), Width = 150, Height = 30, BackColor = Color.LightGreen };
            btnGuardar.Click += BtnGuardar_Click;

            panelForm.Controls.AddRange(new Control[] {
                lblDoc, txtDocumento, lblNom, txtNombre, lblApe, txtApellido,
                lblTel, txtTelefono, lblCorreo, txtCorreo, btnGuardar
            });

            dgvUsuarios = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true };

            this.Controls.Add(dgvUsuarios);
            this.Controls.Add(panelForm);
        }

        private void CargarGrid()
        {
            dgvUsuarios.DataSource = bll.ObtenerUsuarios();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = bll.GuardarUsuario(txtNombre.Text, txtApellido.Text, txtDocumento.Text, txtTelefono.Text, txtCorreo.Text);
                if (ok)
                {
                    MessageBox.Show("Usuario registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrid();
                    txtNombre.Clear(); txtApellido.Clear(); txtDocumento.Clear(); txtTelefono.Clear(); txtCorreo.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    //  PRÉSTAMOS
    public class FormPrestamos : Form
    {
        private BibliotecaBLL bll = new BibliotecaBLL();
        private DataGridView dgvPrestamos;
        private ComboBox cbUsuario, cbLibro;
        private Button btnPrestar, btnDevolver;

        public FormPrestamos()
        {
            this.Text = "Gestión de Préstamos y Devoluciones";
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            BuildUI();
            CargarCombos();
            CargarGrid();
        }

        private void BuildUI()
        {
            Panel panelForm = new Panel { Dock = DockStyle.Top, Height = 100, Padding = new Padding(10) };

            Label lblUsu = new Label { Text = "Usuario:", Location = new Point(20, 25), AutoSize = true };
            cbUsuario = new ComboBox { Location = new Point(90, 22), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblLib = new Label { Text = "Libro:", Location = new Point(360, 25), AutoSize = true };
            cbLibro = new ComboBox { Location = new Point(410, 22), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            btnPrestar = new Button { Text = "Registrar Préstamo", Location = new Point(680, 20), Width = 140, Height = 30, BackColor = Color.Khaki };
            btnPrestar.Click += BtnPrestar_Click;

            btnDevolver = new Button { Text = "Devolver Seleccionado", Location = new Point(680, 58), Width = 140, Height = 30, BackColor = Color.LightCoral };
            btnDevolver.Click += BtnDevolver_Click;

            panelForm.Controls.AddRange(new Control[] {
                lblUsu, cbUsuario, lblLib, cbLibro, btnPrestar, btnDevolver
            });

            dgvPrestamos = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            this.Controls.Add(dgvPrestamos);
            this.Controls.Add(panelForm);
        }

        private void CargarCombos()
        {
            cbUsuario.DataSource = bll.ObtenerUsuarios();
            cbUsuario.DisplayMember = "NombreCompleto";
            cbUsuario.ValueMember = "IdUsuario";

            cbLibro.DataSource = bll.ObtenerLibros();
            cbLibro.DisplayMember = "Titulo";
            cbLibro.ValueMember = "ISBN";
        }

        private void CargarGrid()
        {
            dgvPrestamos.DataSource = bll.ObtenerPrestamos();
        }

        private void BtnPrestar_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = (int)cbUsuario.SelectedValue;
                string isbn = cbLibro.SelectedValue.ToString();

                bool ok = bll.PrestarLibro(idUsuario, isbn);
                if (ok)
                {
                    MessageBox.Show("Préstamo registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrid();
                }
                else
                {
                    MessageBox.Show("No fue posible registrar el préstamo. Verifique las existencias del libro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al prestar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDevolver_Click(object sender, EventArgs e)
        {
            if (dgvPrestamos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un préstamo de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Prestamo p = (Prestamo)dgvPrestamos.SelectedRows[0].DataBoundItem;
                if (p.Estado == "Devuelto")
                {
                    MessageBox.Show("El libro seleccionado ya ha sido devuelto.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool ok = bll.DevolverLibro(p.IdPrestamo, p.ISBN);
                if (ok)
                {
                    MessageBox.Show("Devolución registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en devolución: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    //  (PROGRAM.CS) 
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormPrincipal());
        }
    }
}
