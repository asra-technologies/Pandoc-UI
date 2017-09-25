package Pandoc.UI;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import Pandoc.Native.Operations;

import java.util.logging.ConsoleHandler;

public class Main extends Application {

    static protected Stage stage;

    @Override
    public void start(Stage primaryStage) throws Exception {
        stage = primaryStage;
        Parent root = FXMLLoader.load(getClass().getResource("MainWindow.fxml"));
        primaryStage.setTitle("Pandoc UI");
        primaryStage.setScene(new Scene(root, 800, 450));
        primaryStage.show();
    }


    public static void main(String[] args) {
        if (Operations.checkForPandoc()) {
            System.out.println("true");
        } else {
            System.out.println("false");
        }
        launch(args);
    }
}
