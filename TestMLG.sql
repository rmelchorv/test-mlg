--DROP DATABASE TestMLG;
--GO

--CREATE DATABASE TestMLG;
--GO

USE TestMLG;
GO

/*
DROP TABLE IF EXISTS dbo.ArticuloTienda;
DROP TABLE IF EXISTS dbo.ClienteArticulo;
DROP TABLE IF EXISTS dbo.Compras;
DROP TABLE IF EXISTS dbo.Articulos;
DROP TABLE IF EXISTS dbo.Clientes;
DROP TABLE IF EXISTS dbo.Tienda;
*/
--/*
CREATE TABLE dbo.Clientes (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    apellidos NVARCHAR(100),
    domicilio NVARCHAR(100)
);
CREATE TABLE dbo.Tienda (
    id INT IDENTITY(1,1) PRIMARY KEY,
    sucursal NVARCHAR(50) NOT NULL,
    direccion NVARCHAR(50)
);
CREATE TABLE dbo.Articulos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    codigo NVARCHAR(25) UNIQUE NOT NULL,
    descripcion NVARCHAR(100),
    precio DECIMAL(6, 2) NOT NULL DEFAULT 1.0,
    imagen VARBINARY(max),
    stock INT NOT NULL DEFAULT 0
);
CREATE TABLE dbo.ArticuloTienda (
    id_articulo INT REFERENCES Articulos(id),
    id_tienda INT REFERENCES Tienda(id),
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY(id_articulo, id_tienda)
);
CREATE TABLE dbo.Compras (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT REFERENCES Clientes(id),
    total DECIMAL(6, 2) NOT NULL DEFAULT 0.0,
    fecha DATETIME NOT NULL DEFAULT GETDATE()
);
CREATE TABLE dbo.ClienteArticulo (
    id_compra INT REFERENCES Compras(id),
    id_articulo INT REFERENCES Articulos(id),
    cantidad INT NOT NULL DEFAULT 1,
    PRIMARY KEY(id_compra, id_articulo)
);

GO
--*/
/*
INSERT INTO dbo.Clientes(nombre, apellidos) VALUES('Ricardo E.', 'Melchor');
INSERT INTO dbo.Tienda(sucursal) VALUES('Matriz');
INSERT INTO dbo.Articulos(codigo, descripcion, precio) VALUES('A-123', 'Articulo 1', 10.0),('A-456', 'Articulo 2', 20.0);
INSERT INTO dbo.ArticuloTienda(id_articulo, id_tienda) VALUES(1, 1),(2, 1);
INSERT INTO dbo.Compras(id_cliente) VALUES(1);
INSERT INTO dbo.ClienteArticulo(id_compra, id_articulo) VALUES(1, 1),(1, 2);

GO
*/
/*
SELECT * FROM dbo.Clientes;
*/

GO