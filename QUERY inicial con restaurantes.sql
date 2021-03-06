/****** Script for SelectTopNRows command from SSMS  ******/

/*Estado de Reserva*/
  SET IDENTITY_INSERT [dbReservantes].[dbo].[EstadoReserva] ON
  INSERT INTO [dbReservantes].[dbo].[EstadoReserva] (Id,Descripcion,EstadoReservaEnum) VALUES (1,'Reservado',1)
  INSERT INTO [dbReservantes].[dbo].[EstadoReserva] (Id,Descripcion,EstadoReservaEnum) VALUES (2,'Aceptado',2)
  INSERT INTO [dbReservantes].[dbo].[EstadoReserva] (Id,Descripcion,EstadoReservaEnum) VALUES (3,'Pagado',3)
  INSERT INTO [dbReservantes].[dbo].[EstadoReserva] (Id,Descripcion,EstadoReservaEnum) VALUES (4,'Rechazado',4)
  SET IDENTITY_INSERT [dbReservantes].[dbo].[EstadoReserva] OFF
  GO
  /*DatosBancarios para Restaurante 1-Falta Número de cuenta y CBU debe ser bigint */
  SET IDENTITY_INSERT [dbReservantes].[dbo].[DatosBancarios] ON
  INSERT INTO [dbReservantes].[dbo].[DatosBancarios] (Id,NumeroCuenta,CBU) VALUES (1,251865658888888,123548484845454)
  INSERT INTO [dbReservantes].[dbo].[DatosBancarios] (Id,NumeroCuenta,CBU) VALUES (2,232659595959595959595,846262626262662)

  SET IDENTITY_INSERT [dbReservantes].[dbo].[DatosBancarios] OFF
  GO
    /*Piso y departamento deben ser nuleables*/
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Domicilio] ON
  /*Restaurantes*/
  INSERT INTO [dbReservantes].[dbo].[Domicilio] (Id,NombreCalle,NumeroCalle,NumeroPiso,NumeroDpto,LocalidadId,Ubicacion)
  VALUES (1,'Avenida de Mayo', 222,1,2,764, geography::Point(-34.642850400, -58.565065500 , 4326 ))
  INSERT INTO [dbReservantes].[dbo].[Domicilio] (Id,NombreCalle,NumeroCalle,NumeroPiso,NumeroDpto,LocalidadId,Ubicacion)
  VALUES (3,'Rosales', 220,1,1,764, geography::Point(-34.643773400, -58.565605000, 4326 ))
  /*Cliente*/
  INSERT INTO [dbReservantes].[dbo].[Domicilio] (Id,NombreCalle,NumeroCalle,NumeroPiso,NumeroDpto,LocalidadId,Ubicacion)
  VALUES (2,'Espora', 222,1,2,764, geography::Point(-34.644599200, -58.565227700, 4326 ))
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Domicilio] OFF
  GO
  update [dbReservantes].[dbo].[Domicilio] set Latitud=-34.644599200, Longitud=-58.565227700 where Id=2
  update [dbReservantes].[dbo].[Domicilio] set Latitud=-34.643773400, Longitud=-58.565605000 where Id=3

  /*usuario para segundo restaurante*/
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Usuario] ON
  insert into [dbReservantes].[dbo].[Usuario] (Id,TipoUsuarioId,Email,Password,Username)
  VALUES (4,3,'restaurante@gmail.com','123parrilla','Restaurante')
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Usuario] OFF

    /*Agrega un cliente*/
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Cliente] ON
  INSERT INTO [dbReservantes].[dbo].[Cliente](IdCliente,IdUsuario, Nombre, Apellido, DomicilioId)
  values (1,2,'Pablo','Perez',2)
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Cliente] OFF

  /*Nuevo restaurante-CUIT vacío, falta tabla estado*/
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Restaurante] ON
  INSERT INTO [dbReservantes].[dbo].[Restaurante] 
  (IdRestaurante,IdUsuario,CUIT,RazonSocial,NombreComercial,DatosBancariosId,CantidadClientes,Habilitado,DomicilioID)
  values(1,3,'23-36274805-4','Gourmet SRL','Gourmet',1,20,1,1)
  INSERT INTO [dbReservantes].[dbo].[Restaurante] 
  (IdRestaurante,IdUsuario,CUIT,RazonSocial,NombreComercial,DatosBancariosId,CantidadClientes,Habilitado,DomicilioID)
  values(2,4,'23-36274805-4','Parilla SA','Parrilla',2,50,1,3)
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Restaurante] Off


  /*Corrige usuario restaurante y crea usuario admin*/
  UPDATE [dbReservantes].[dbo].[Usuario] SET TipoUsuarioId=3 WHERE Id=4
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Usuario] ON
  insert into [dbReservantes].[dbo].[Usuario] (Id,TipoUsuarioId,Email,Password,Username)
  VALUES (5,1,'Admin@reservantes.com','admin666','Admin')
  SET IDENTITY_INSERT [dbReservantes].[dbo].[Usuario] OFF
