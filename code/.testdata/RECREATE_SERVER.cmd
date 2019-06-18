@ECHO OFF

SETLOCAL

SET ROOT=%~dp0
SET SERVER=DF_SERVER
SET DB=DF_TEST_DB
SET MDF=%ROOT%\%DB%.mdf
SET LDF=%ROOT%\%DB%.ldf

ECHO Cleaning up ...
sqllocaldb stop %SERVER%    > NUL
sqllocaldb delete %SERVER%  > NUL
del %MDF% > NUL
del %LDF% > NUL

ECHO Recreating %SERVER% ...
sqllocaldb create %SERVER%  > NUL
sqllocaldb start %SERVER%   > NUL

ECHO Recreating DB ...
sqlcmd -v DB = "%DB%" -v MDF = "%MDF%" -v LDF = "%LDF%" -S (localdb)\%SERVER% -i "%ROOT%\CREATE_DF_TEST_DB.sql"

ECHO Done.

ENDLOCAL