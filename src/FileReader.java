import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.HashMap;
import java.util.HashSet;

public class FileReader {
    HashMap<String, HashSet<Integer>> indexes = new HashMap<>();
    private String pathName;

    public FileReader(String pathName) {
        this.pathName = pathName;
    }

    private boolean readDataFromFile(File fileToRead) {
        String data;
        data = readContents(fileToRead);
        if (!isFileFound(data))
            return false;
        String[] words = processDocumentAndGiveWords(data);
        storeWords(words, fileToRead);
        return true;
    }

    private boolean isFileFound(String data) {
        return data != null;
    }

    private String[] processDocumentAndGiveWords(String data) {
        DocumentProcessor documentProcessor = new DocumentProcessor(data);
        return documentProcessor.getNormalizedWords();
    }

    private void storeWords(String[] words, File file) {
        for (String word : words) {
            HashSet<Integer> fileIds = indexes.computeIfAbsent(word, wordIndex -> new HashSet<>());
            fileIds.add(Integer.valueOf(file.getName()));
        }
    }

    private String readContents(File fileToRead) {
        try {
            Path path = Path.of(fileToRead.getAbsolutePath());
            return new String(Files.readAllBytes(path));
        } catch (IOException e) {
            System.out.println("File doesn't exist!");
            return null;
        }

    }

    public HashMap<String, HashSet<Integer>> fillIndexes() {
        File[] files = importFiles();
        readFiles(files);
        return indexes;
    }

    private void readFiles(File[] files) {
        for (File fileToRead : files) {
            if (!readDataFromFile(fileToRead))
                System.out.println("couldn't read data");
        }
    }

    private File[] importFiles() {
        File file = new File(pathName);
        File[] files = file.listFiles();
        return files;
    }
}
