﻿// Di Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<ReservationManager>();
    services.AddSingleton<TransactionManager>();
    // Registrasi lainnya...
}