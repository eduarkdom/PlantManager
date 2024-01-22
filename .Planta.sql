

-- CREACIÓN DE LA BASE DE DATOS.
CREATE DATABASE El_Salto;
GO

USE El_Salto;
GO

-- CREACIÓN DE LA TABLA LOGIN.
CREATE TABLE Login (
    Id INT IDENTITY(1,1),
    Username VARCHAR(25) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
	CONSTRAINT Pk_Login_Id PRIMARY KEY (Id)
);
GO

-- CREACIÓN DE LA VISTA QUE PERMITE OBTENER TODOS LOS REGISTROS DE LA TABLA LOGIN.
CREATE VIEW vwGetLogins
AS
    SELECT * FROM Login;
GO

--CREACIÓN DE PROCEDIMIENTO ALMACENADO LOGIN
CREATE PROCEDURE spLogin
	@username VARCHAR(255)
AS
	select Password from login where Username = @username
GO
--CREACIÓN DE PROCEDIMIENTO ALMACENADO PARA CREAR USUARIO
CREATE PROCEDURE spCreateUser
	@username VARCHAR(255),
	@password VARCHAR(255)
AS
	INSERT INTO Login(Username, Password) VALUES (@username, @password)
GO
--EJECUTAR PROCEDIMIENTO DE CREACIÓN DE USUARIO O USAR INSERT INTO
DECLARE @user VARCHAR(255) = 'Administrador'
DECLARE @pass VARCHAR(255) = 'uUWaCI3ScZ6IzyWLXdoIIdUkWr1/1fjrwiYaDoPUQ7SJ5hPj'
/*pass es 123456*/
EXEC spCreateUser @user, @pass
GO
--EJECUTAR INSERT INTO PARA AGREGAR USUARIO ADMINISTRADOR
INSERT INTO Login (Username, Password) VALUES ('Administrador', 'uUWaCI3ScZ6IzyWLXdoIIdUkWr1/1fjrwiYaDoPUQ7SJ5hPj');

-- CREACIÓN DE LA TABLA PLANTA.
CREATE TABLE Planta (
    Id INT IDENTITY(1,1),
    NombreComun VARCHAR(255) UNIQUE NOT NULL,
    NombreCientifico VARCHAR(255) UNIQUE NOT NULL,
    TipoPlanta VARCHAR(255) NOT NULL,
    Descripcion VARCHAR(255),
    TiempoRiego INT NOT NULL,
    CantidadAgua INT NOT NULL,
    Epoca VARCHAR(15) NOT NULL,
    EsVenenosa BIT,
    EsAutoctona BIT,
	CONSTRAINT Pk_Planta_Id PRIMARY KEY (Id)
);
GO

-- CREACIÓN DE LA VISTA QUE PERMITE OBTENER TODOS LOS REGISTROS DE LA TABLA PLANTA.
CREATE VIEW vwGetPlantas
AS
    SELECT * FROM Planta;
GO

-- CREACIÓN DE LA FUNCIÓN QUE RETORNA LA CANTIDAD DE PLANTAS DE UN TIPO ESPECÍFICO.
CREATE FUNCTION fxObtenerCantidadPlantasPorTipo
    (@TipoPlanta VARCHAR(255))
RETURNS INT
AS
BEGIN
    DECLARE @res INT;
    SELECT @res = COUNT(*) FROM Planta WHERE TipoPlanta = @TipoPlanta;
    RETURN @res;
END;
GO


-- CREACIÓN DEL PROCEDIMIENTO ALMACENADO PARA GUARDAR PLANTA.
CREATE PROCEDURE spPlantaSave
    @NombreComun VARCHAR(255),
    @NombreCientifico VARCHAR(255),
    @TipoPlanta VARCHAR(255),
    @Descripcion VARCHAR(255),
    @TiempoRiego INT,
    @CantidadAgua INT,
    @Epoca VARCHAR(15),
    @EsVenenosa BIT,
    @EsAutoctona BIT
AS
BEGIN
    INSERT INTO Planta (NombreComun, NombreCientifico, TipoPlanta, Descripcion, TiempoRiego, CantidadAgua, Epoca, EsVenenosa, EsAutoctona)
    VALUES (@NombreComun, @NombreCientifico, @TipoPlanta, @Descripcion, @TiempoRiego, @CantidadAgua, @Epoca, @EsVenenosa, @EsAutoctona);
END;
GO

-- CREACIÓN DEL PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR PLANTA EN BASE A SU ID.
CREATE PROCEDURE spPlantaUpdateById
    @PlantaId INT,
    @NombreComun VARCHAR(255),
    @NombreCientifico VARCHAR(255),
    @TipoPlanta VARCHAR(255),
    @Descripcion VARCHAR(255),
    @TiempoRiego INT,
    @CantidadAgua INT,
    @Epoca VARCHAR(15),
    @EsVenenosa BIT,
    @EsAutoctona BIT
AS
BEGIN
    UPDATE Planta
    SET NombreComun = @NombreComun,
        NombreCientifico = @NombreCientifico,
        TipoPlanta = @TipoPlanta,
        Descripcion = @Descripcion,
        TiempoRiego = @TiempoRiego,
        CantidadAgua = @CantidadAgua,
        Epoca = @Epoca,
        EsVenenosa = @EsVenenosa,
        EsAutoctona = @EsAutoctona
    WHERE Id = @PlantaId;
END;
GO

-- CREACIÓN DEL PROCEDIMIENTO ALMACENADO PARA ELIMINAR PLANTA EN BASE A SU ID.
CREATE PROCEDURE spPlantaDeleteById
    @PlantaId INT
AS
BEGIN
    DELETE FROM Planta WHERE Id = @PlantaId;
END;
GO

-- CREACIÓN DEL PROCEDIMIENTO ALMACENADO PARA RETORNAR LA CANTIDAD DE PLANTAS DE UN TIPO ESPECÍFICO.
CREATE PROCEDURE spObtenerCantidadPlantasPorTipo
    @TipoPlanta VARCHAR(255)
AS
BEGIN
    SELECT dbo.fxObtenerCantidadPlantasPorTipo(@TipoPlanta);
END;
GO
