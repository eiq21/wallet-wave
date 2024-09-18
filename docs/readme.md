### Propuesta Detallada de Solución para la Billetera Digital

La propuesta incluye una solución de arquitectura escalable para una **billetera digital**, construida sobre **microservicios**, **tecnologías cloud** de **Azure** y conectividad **on-premise** mediante **ExpressRoute**. Este nivel de detalle proporcionará una mayor comprensión de los componentes clave, casos de uso específicos, y las decisiones tecnológicas que respaldan el diseño.

---

### 1. **Arquitectura de Capa Canal: Microservicios UX (MS UX)**

Los **MS UX** son los microservicios que gestionan la experiencia de usuario final. Están diseñados para exponer APIs a través de **API Gateway UX** y se encargan de manejar las interacciones de los clientes con la billetera digital. Estas APIs son accesibles desde aplicaciones móviles y web.

#### 1.1 **Componentes Principales de la Capa Canal**

- **API UX Wallet Mobile (iOS/Android):** Maneja las operaciones de la billetera digital para las aplicaciones móviles. Incluye consultas de saldo, transferencias, y gestión de cuentas.
- **API UX Wallet Web:** Maneja la interfaz web para los usuarios que acceden a través de navegadores.
- **API UX Login:** Unifica los servicios de autenticación y autorización a través de múltiples plataformas, utilizando **OAuth 2.0** y **CIAM** (Customer Identity and Access Management).

#### 1.2 **Casos de Uso Principales de MS UX**

- **Autenticación y Registro de Usuarios:** Permite a los usuarios registrarse y acceder a su billetera utilizando sus credenciales. Este proceso está asegurado mediante autenticación multifactor (MFA) y tokens de acceso.
- **Gestión de Saldo:** Consultar y visualizar el saldo disponible en tiempo real.
- **Transferencias y Pagos:** Enviar dinero a otros usuarios, realizar pagos a terceros y pagar servicios, todo gestionado a través de los **MS BS** de negocio.
- **Notificaciones:** Enviar alertas al dispositivo móvil del usuario o al correo electrónico sobre movimientos importantes, utilizando el **Notification MS UX**.

#### 1.3 **Decisiones Tecnológicas**

- **CosmosDB** fue elegido como base de datos para los **MS UX** por su flexibilidad en el manejo de datos no estructurados, baja latencia y alta disponibilidad, lo que lo hace ideal para las interacciones en tiempo real con los usuarios finales.

---

### 2. **Arquitectura de Negocio: Microservicios BS (MS BS)**

Los **MS BS** son los microservicios donde reside la lógica de negocio central. Estos servicios son responsables de las transacciones financieras, seguridad y reglas de negocio. Los **API Gateway BS** permiten la interacción entre los **MS UX** y los **MS BS**, desacoplando la lógica de presentación y de negocio.

#### 2.1 **Componentes Principales de la Capa Negocio**

- **Wallet MS BS:** Maneja la creación, actualización y eliminación de las billeteras digitales de los usuarios.
- **Transaction MS BS:** Procesa las transacciones de dinero entre cuentas, gestionando la lógica de las transferencias, pagos, y liquidaciones.
- **Security MS BS:** Controla la autenticación avanzada, manejo de tokens, roles, y políticas de acceso.
- **Notification MS BS:** Responsable de orquestar las notificaciones automáticas sobre actividades importantes en la cuenta del usuario.

#### 2.2 **Casos de Uso Principales de MS BS**

- **Validación de Transacciones:** Verifica la autenticidad y validez de las transacciones antes de procesarlas. Se aseguran las políticas de antifraude y validaciones conforme a las reglas del negocio.
- **Conciliación y Auditoría:** Los registros de transacciones son guardados y auditados para cumplir con los requerimientos regulatorios y de seguridad.
- **Seguridad Avanzada:** Implementa políticas de **OAuth 2.0**, **JWT** y mecanismos de MFA para asegurar las transacciones.
- **Envió de Notificaciones Personalizadas:** Notifica a los usuarios sobre transferencias, pagos exitosos o fallidos, o cualquier movimiento relevante en la cuenta.

#### 2.3 **Decisiones Tecnológicas**

- **PostgreSQL** fue seleccionado para las **transacciones financieras** debido a su capacidad para gestionar operaciones de alta frecuencia con integridad transaccional. PostgreSQL soporta **ACID**, ideal para operaciones bancarias que requieren consistencia y confiabilidad.
- **SQL Server** se utiliza para manejar datos estructurados de alto volumen en los servicios de seguridad, debido a su robustez en la administración de registros y su integración con soluciones empresariales existentes.

---

### 3. **Capa Core: Integración y Backend Centralizado**

La capa **Core** de la billetera es el núcleo que permite la integración con servicios compartidos del banco y con sistemas legacy on-premise. Los **APIs Core** permiten que los sistemas internos interactúen con los **MS BS** de negocio para llevar a cabo las operaciones más críticas del banco.

#### 3.1 **Componentes Principales de la Capa Core**

- **APIS Core:** Proporciona servicios compartidos como la administración centralizada de cuentas y registros. Estos servicios son reutilizables por los **MS BS** y otros sistemas del banco.
- **MS Core:** Se encarga de la integración directa con el sistema on-premise. Maneja las solicitudes y respuestas de los sistemas legacy mediante **ExpressRoute** y **ZConnect** para comunicar las aplicaciones modernas con las soluciones legacy.

#### 3.2 **Casos de Uso Principales de la Capa Core**

- **Integración con sistemas on-premise:** Permite que los sistemas como el ERP del banco, que se ejecutan on-premise, interactúen con la solución cloud para obtener información de usuarios y transacciones.
- **Operaciones Bancarias Legacy:** A través de **ZConnect**, los sistemas legacy pueden ejecutar operaciones críticas que aún no han sido migradas a la nube.
- **Monitoreo y Gestión Centralizada:** Proporciona herramientas para monitorear las transacciones en todos los servicios y generar reportes diarios o mensuales.

#### 3.3 **Decisiones Tecnológicas**

- **CosmosDB** también se utiliza en la capa Core para almacenar datos no estructurados y flexibles, como logs y auditorías de transacciones.
- **Azure Event Hub**: Se usa un **bus de eventos (Pub/Sub)** que facilita la comunicación entre los microservicios y permite la escalabilidad en el envío de mensajes y eventos entre servicios distribuidos.

---

### 4. **Integración con la Nube y On-Premise**

#### 4.1 **ExpressRoute y ZConnect**

Para asegurar una conectividad rápida, confiable y segura entre la infraestructura on-premise del banco y la nube de Azure, se utiliza **ExpressRoute**. Este servicio proporciona conexiones privadas entre Azure y la infraestructura on-premise, con **bajas latencias** y garantizando la seguridad de la información financiera sensible.

**ZConnect** permite exponer las APIs REST de los sistemas legacy, facilitando la migración gradual a la nube sin interrumpir los servicios existentes.

#### 4.2 **CIAM (Customer Identity and Access Management)**

Para la gestión centralizada de usuarios y autenticación, se emplea **CIAM**, lo que asegura la autenticación de usuarios con políticas de seguridad avanzadas, como la gestión de identidades y control de acceso.

#### 4.3 **KeyVault**

Azure **KeyVault** almacena de forma segura las claves y secretos de la aplicación, garantizando que las credenciales, claves API y certificados estén protegidos y puedan ser gestionados a través de una interfaz centralizada y segura.

---

### 5. **Escalabilidad y Resiliencia**

#### 5.1 **Azure Kubernetes Service (AKS)**

El uso de **AKS** permite la gestión y orquestación de los microservicios, garantizando **alta disponibilidad** y **escalabilidad automática** para manejar incrementos en la demanda de usuarios.

#### 5.2 **Event Bus (Pub/Sub)**

El **bus de eventos** permite la **comunicación asíncrona** entre microservicios, facilitando el desacoplamiento de las operaciones y mejorando el rendimiento del sistema cuando se producen grandes volúmenes de eventos simultáneamente.

#### 5.3 **Distribución y Cache**

Una **cache distribuida** mejora el rendimiento general del sistema, ya que los datos críticos y de alta frecuencia (como el saldo de la cuenta o las últimas transacciones) se almacenan temporalmente para una respuesta más rápida a las consultas de los usuarios.

---

### 6. **Seguridad y Cumplimiento**

#### 6.1 **Protección de Datos en Tránsito y Reposo**

Todos los datos que se transmiten entre los componentes de la billetera digital están cifrados mediante **TLS** y **AES-256**, asegurando la protección de la información en tránsito y en reposo, cumpliendo con las normativas de seguridad bancarias.

#### 6.2 **Autenticación Avanzada**

El uso de **CIAM** y **OAuth 2.0** permite una gestión segura de identidades, protegiendo a los usuarios contra accesos no autorizados. Además, el **multifactor authentication (MFA)** añade una capa adicional de seguridad para operaciones sensibles.

---

### Conclusión

La solución propuesta aprovecha lo mejor de la tecnología cloud en **Azure**, con una arquitectura **basada en microservicios** y **alta escalabilidad**. El diseño asegura una **alta disponibilidad**, **seguridad avanzada** y una **integración fluida** entre servicios cloud y sistemas legacy on-premise.
