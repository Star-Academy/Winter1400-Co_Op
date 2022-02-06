import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.HashMap;
import java.util.HashSet;

public class FileReader {

    private HashMap<String, HashSet<Integer>> indexes = new HashMap<>();
    private static FileReader instance = null;
    private FileReader(){}

    public static FileReader getInstance(){
        if (instance == null){
            instance = new FileReader();
        }
        return instance;
    }

    private void readDataFromFile(File fileToRead){

        String data;
        try {
            data = new String(Files.readAllBytes(Path.of(fileToRead.getAbsolutePath())));
            DocumentProcessor documentProcessor = new DocumentProcessor(data);
            String[] strings = documentProcessor.getNormalizedWords();
            for (String word : strings) {
                indexes.computeIfAbsent(word, wordIndex -> new HashSet<>()).add(Integer.valueOf(fileToRead.getName()));
            }
        } catch (IOException e) {
            System.out.println("File doesn't exist!");
        }
    }

    public  HashMap<String, HashSet<Integer>> fillIndexes() {
        File file = new File("files");
        File[] files = file.listFiles();
        assert files != null;
        for (File fileToRead : files) {
            readDataFromFile(fileToRead);
        }
        return indexes;
    }
}
