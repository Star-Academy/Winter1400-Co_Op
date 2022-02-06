import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.HashMap;
import java.util.HashSet;

public class FileReader {
    HashMap<String, HashSet<Integer>> indexes = new HashMap<>();
    public void readDataFromFile(File fileToRead){
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
    public void fillIndexes() {
        File file = new File("files");
        File[] files = file.listFiles();
        assert files != null;
        for (File fileToRead : files) {
            readDataFromFile(fileToRead);
        }
    }
    public HashSet<Integer> getIndex(String word){
        return indexes.get(word);
    }

}
