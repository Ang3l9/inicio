using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using ReservacionVehicleBD.Data;
using ReservacionVehicleBD.Models;
using System.Linq;

namespace ReservacionVehicleBD.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AuthService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public void RegistrarUsuario(string nombreUsuario, string contrasena)
        {
            // Verificar si el usuario ya existe
            var existingUser = _dbContext.Usuarios.SingleOrDefault(u => u.NombreUsuario == nombreUsuario);
            if (existingUser != null)
            {
                throw new Exception("El nombre de usuario ya está en uso."); // Lanzar excepción si el usuario ya existe
            }

            // Crear el objeto usuario primero
            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
            };

            // Hashear la contraseña
            usuario.Contraseña = _passwordHasher.HashPassword(usuario, contrasena);

            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
        }

        public bool ValidarUsuario(string nombreUsuario, string contrasena)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(u => u.NombreUsuario == nombreUsuario);
            if (usuario == null)
                return false;

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contraseña, contrasena);
            return resultado == PasswordVerificationResult.Success;
        }
        public bool UsuarioExiste(string nombreUsuario)
        {
            return _dbContext.Usuarios.Any(u => u.NombreUsuario == nombreUsuario);
        }

    }
}
