package Pandoc.Native;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.StringReader;
import java.lang.Process;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class Operations {
    private static String inputPath;
    private static String outputPath;
    private static String args;

    public static boolean checkForPandoc() {
        try {
            String line;
            Process pandoc = new ProcessBuilder("pandoc", "-v").start();
            BufferedReader input = new BufferedReader(new InputStreamReader(pandoc.getInputStream()));
            while ((line = input.readLine()) != null) {
                System.out.println(line);
            }
            input.close();
            return true;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return false;
    }

    public static void setFileLocations(String input, String output) {
        inputPath = input;
        outputPath = output;
    }

    public static void executeCommand() {
        try {
            ArrayList<String> process = createCommand();
            Process pandoc = new ProcessBuilder(process).start();
            //int i = pandoc.exitValue();
            String line;
            BufferedReader output = new BufferedReader(new InputStreamReader(pandoc.getErrorStream()));
            while ((line = output.readLine()) != null) {
                System.out.println(line);
            }
            output.close();
        } catch (IOException e) {

        }
    }

    private static ArrayList<String> createCommand() {
        ArrayList<String> command = new ArrayList<String>();
        command.add("pandoc");
        command.add("-s");
        command.add(inputPath);
        command.add("-o");
        command.add(outputPath);
        return command;
    }
}
