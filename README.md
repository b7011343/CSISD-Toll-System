# CSISD-Toll-System

![CI](https://github.com/b7011343/CSISD-Toll-System/actions/workflows/ci.yml/badge.svg)

Run this line to call the first migration in the package manager console:
```
  update-database -context ApplicationDbContext -migration 00000000000000_CreateIdentitySchema
```
To drop the database tables run the following command in the package manager console:
```
  drop-database -Context ApplicationDbContext
```
