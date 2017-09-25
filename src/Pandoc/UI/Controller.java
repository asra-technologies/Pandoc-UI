package Pandoc.UI;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;

import java.io.File;

import javafx.stage.FileChooser;

import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;

import java.awt.*;

public class Controller {
    private Desktop desktop = Desktop.getDesktop();

    final FileChooser fileChooser = new FileChooser();

    @FXML
    protected void setInputFile(ActionEvent event) {
        configureFileChooserInput(fileChooser);
        File file = fileChooser.showOpenDialog(Main.stage);
        if (file != null) {
            openFile(file);
        }
    }

    @FXML
    protected void setOutputFile(ActionEvent event) {
        configureFileChooserSaveFormat(fileChooser);
        File file = fileChooser.showSaveDialog(Main.stage);
        if (file != null) {
            openFile(file);
        }
    }

    private void openFile(File file) {
        try {
            desktop.open(file);
        } catch (IOException ex) {
            Logger.getLogger(
                    Controller.class.getName()).log(
                    Level.SEVERE, null, ex
            );
        }
    }

    private static void configureFileChooserSaveFormat(final FileChooser fileChooser) {
        fileChooser.setTitle("Save output");
        fileChooser.setInitialDirectory(new File(System.getProperty("user.home")));
        fileChooser.getExtensionFilters().setAll(
                new FileChooser.ExtensionFilter("PDF (*.pdf)", "*.pdf"),
                new FileChooser.ExtensionFilter("Word Document (*.docx)", "*.docx"),
                new FileChooser.ExtensionFilter("Html file (*.html)", "*.html")
        );
    }

    private static void configureFileChooserInput(final FileChooser fileChooser) {
        fileChooser.setTitle("Input file");
        fileChooser.setInitialDirectory(new File(System.getProperty("user.home")));
        fileChooser.getExtensionFilters().setAll(
                new FileChooser.ExtensionFilter("Any File", "*.*")
        );
    }
}
