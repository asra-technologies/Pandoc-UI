package Pandoc.UI;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;

import java.io.File;

import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TextField;
import javafx.stage.FileChooser;

import Pandoc.Native.Operations;

public class Controller {

    final FileChooser fileChooser = new FileChooser();

    @FXML
    private TextField inputFile;
    @FXML
    private TextField outputFile;
    private boolean inputSet = false;
    private boolean outputSet = false;

    @FXML
    private ComboBox<? extends String> outputFormat;

    @FXML
    private Button convertButton;

    @FXML
    public void initialize() {
        if (Main.arguments.length > 0) {
            inputFile.setText(Main.arguments[0]);
        }
        if (Main.arguments.length > 1) {
            outputFile.setText(Main.arguments[1]);
        }
    }

    @FXML
    protected void setInputFile(ActionEvent event) {
        configureFileChooserInput(fileChooser);
        File file = fileChooser.showOpenDialog(Main.stage);
        if (checkIfFileExists(file)) {
            setFilePath(false, file);
            setConvertVisible(false);
        }
    }

    @FXML
    protected void setOutputFile(ActionEvent event) {
        configureFileChooserSaveFormat(fileChooser);
        File file = fileChooser.showSaveDialog(Main.stage);
        if (checkIfFileExists(file)) {
            setFilePath(true, file);
            setConvertVisible(true);
        }
    }

    @FXML
    protected void formatSelected() {
        System.out.println("Boop" + outputFormat.getValue());
    }

    @FXML
    protected void convertDocument(ActionEvent event) {
        Operations.setFileLocations(inputFile.getText(), outputFile.getText());
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

    private boolean checkIfFileExists(File file) {
        return file != null;
    }

    private void setFilePath(boolean isOutput, File file) {
        if (isOutput) {
            outputFile.setText(file.getPath());
            outputSet = true;
        } else {
            inputFile.setText(file.getPath());
            inputSet = true;
        }
    }

    private void setConvertVisible(boolean isOutput) {
        if (inputSet && outputSet) {
            convertButton.setVisible(true);
        }
    }
}
