package Pandoc.Native;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.lang.Process;
import java.io.IOException;

public class Operations {
    public static boolean checkForPandoc(){
        try {
            String line;
            Process pandoc = new ProcessBuilder("pandoc", "-v").start();
            BufferedReader input =
                    new BufferedReader
                            (new InputStreamReader(pandoc.getInputStream()));
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
}
