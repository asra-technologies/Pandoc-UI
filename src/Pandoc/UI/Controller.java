package Pandoc.UI;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.text.Text;

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
        File file = fileChooser.showOpenDialog(Main.stage);
        if (file != null) {
            openFile(file);
        }
    }

    @FXML
    protected void setOutputFile(ActionEvent event) {
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

    private static void configureFileChooser(final FileChooser fileChooser) {
        fileChooser.setTitle("View Pictures");
        fileChooser.setInitialDirectory(new File(System.getProperty("user.home")));
        fileChooser.getExtensionFilters().addAll(
                new FileChooser.ExtensionFilter("All Images", "*.*"),
                new FileChooser.ExtensionFilter("JPG", "*.jpg"),
                new FileChooser.ExtensionFilter("PNG", "*.png")
        );
    }
}
