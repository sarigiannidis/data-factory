@echo off

call support\cleanup.cmd
call support\create_demo_server.cmd

sqlcmd -S (localdb)\demo_server -i "sql\create demo_db.sql"