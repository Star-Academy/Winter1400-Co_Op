import java.util.Scanner;

public class View {

    Controller controller;

    public View(){
        controller = new Controller(new FileReader("files"));
    }

    public void run(){
        scanQuery();
        System.out.println(controller.getFileIdsMatchesZeroAndPlusAndMinusQueries());
    }

    private void scanQuery(){
        System.out.println("Enter the query:");
        Scanner scanner = new Scanner(System.in);
        controller.setQuery(new Queries(scanner.nextLine()));
    }

}
