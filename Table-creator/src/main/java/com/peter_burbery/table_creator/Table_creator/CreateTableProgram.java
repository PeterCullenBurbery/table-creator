/**
 * @since 2024-W41-3 20.54.12.686 -0400
 * @author peter
 */
package com.peter_burbery.table_creator.Table_creator;

/**
 * 
 */
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class CreateTableProgram {
    private static final String DB_PASSWORD = "1234";

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Connection connection = null;

        System.out.print("Enter output directory for SQL files: ");
        String outputDirectoryPath = scanner.nextLine().trim();
        File outputDirectory = new File(outputDirectoryPath);
        if (!outputDirectory.exists() && !outputDirectory.mkdirs()) {
            System.err.println("Failed to create output directory.");
            return;
        }

        try {
            // Connect to CDB as SYSDBA
            connection = getConnection("orcl.localdomain", true);
            System.out.println("Connected to Oracle as SYSDBA.");

            while (true) {
                System.out.print("Enter the PDB to connect to: ");
                String pdb = scanner.nextLine().trim();
                Pattern pattern = Pattern.compile("^[a-zA-Z0-9_]+$");
                Matcher matcher = pattern.matcher(pdb.split("\\.")[0]);
                if (matcher.find()) {
                    try (Statement stmt = connection.createStatement()) {
                        // Debugging: Print current user information
                        try (ResultSet rs = stmt.executeQuery("SELECT SYS_CONTEXT('USERENV', 'SESSION_USER') AS current_user FROM dual")) {
                            if (rs.next()) {
                                System.out.println("Current connected user: " + rs.getString("current_user"));
                            }
                        }
                        stmt.execute("ALTER SESSION SET CONTAINER = " + pdb.split("\\.")[0]);
                        System.out.println("Connected to PDB: " + pdb);

                        System.out.print("Enter the username to use for executing statements on the PDB: ");
                        String user = scanner.nextLine().trim();

                        // Connect to PDB as specified user
                        connection = getConnection(pdb + ".localdomain", user);
                        System.out.println("Connected to PDB as user: " + user);
                        break;
                    } catch (SQLException e) {
                        System.err.println("PDB not found or incorrect: " + pdb);
                    }
                } else {
                    System.err.println("Invalid PDB name. Please enter again.");
                }
            }

            while (true) {
                System.out.print("Enter a table name (or type 'exit', 'quit', 'stop' to finish): ");
                String tableName = scanner.nextLine().trim();

                if (tableName.equalsIgnoreCase("exit") ||
                    tableName.equalsIgnoreCase("quit") ||
                    tableName.equalsIgnoreCase("stop")) {
                    System.out.println("Exiting program.");
                    break;
                }

                String sql = "CREATE TABLE " + tableName + " (" +
                             tableName + "_id RAW(16) DEFAULT sys_guid() PRIMARY KEY, " +
                             tableName + " VARCHAR2(4000), " +
                             "note VARCHAR2(4000), " +
                             "date_created TIMESTAMP(9) WITH TIME ZONE DEFAULT systimestamp(9) NOT NULL, " +
                             "date_updated TIMESTAMP(9) WITH TIME ZONE, " +
                             "date_created_or_updated TIMESTAMP(9) WITH TIME ZONE GENERATED ALWAYS AS (coalesce(date_updated, date_created)) VIRTUAL " +
                             ");";

                String triggerName = "date_updated_" + tableName;
                String triggerSql = "CREATE OR REPLACE TRIGGER " + triggerName + " " +
                                    "    BEFORE UPDATE ON " + tableName + " " +
                                    "    FOR EACH ROW " +
                                    "BEGIN " +
                                    "    :new.date_updated := systimestamp; " +
                                    "END;";

                String fullSql = sql + "\n" + triggerSql;

                // Save SQL to file before executing
                saveSqlToFile(outputDirectory, tableName, fullSql);

                try (Statement stmt = connection.createStatement()) {
                    // Execute SQL from saved file
                    File sqlFile = new File(outputDirectory, tableName.replace("_", "-") + ".sql");
                    try (Scanner fileScanner = new Scanner(sqlFile)) {
                        StringBuilder sqlFromFile = new StringBuilder();
                        while (fileScanner.hasNextLine()) {
                            sqlFromFile.append(fileScanner.nextLine()).append("\n");
                        }
                        String finalSql = sqlFromFile.toString().trim();
                        System.out.println("Executing SQL: " + finalSql);
                        stmt.executeUpdate(finalSql);
                    }
                    System.out.println("SQL executed successfully: " + fullSql);
                } catch (SQLException e) {
                    System.err.println("Failed to create table: " + e.getMessage());
                    e.printStackTrace();
                }
            }

        } catch (SQLException e) {
            System.err.println("Failed to connect to Oracle: " + e.getMessage());
        } finally {
            try {
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                System.err.println("Failed to close connection: " + e.getMessage());
            }
        }
    }

    // Method to establish a connection to the Oracle Database
    private static Connection getConnection(String serviceName, boolean isSysDBA) throws SQLException {
        String url = "jdbc:oracle:thin:@localhost:1521/" + serviceName;
        String user = isSysDBA ? "sys as sysdba" : "system";
        return DriverManager.getConnection(url, user, DB_PASSWORD);
    }

    // Overloaded method to establish a connection with a specified user
    private static Connection getConnection(String serviceName, String user) throws SQLException {
        String url = "jdbc:oracle:thin:@localhost:1521/" + serviceName;
        return DriverManager.getConnection(url, user, DB_PASSWORD);
    }

    private static void saveSqlToFile(File outputDirectory, String tableName, String sql) {
        String filename = tableName.replace("_", "-") + ".sql";
        File file = new File(outputDirectory, filename);

        try (FileOutputStream fos = new FileOutputStream(file)) {
            fos.write(sql.getBytes());
            fos.flush();

            DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
            String timestamp = LocalDateTime.now().format(formatter);

            System.out.println("SQL saved to file: " + file.getAbsolutePath());
            System.out.println("File size: " + file.length() + " bytes");
            System.out.println("Timestamp: " + timestamp);
        } catch (IOException e) {
            System.err.println("Failed to save SQL to file: " + e.getMessage());
        }
    }
}