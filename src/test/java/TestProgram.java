import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.util.*;


public class TestProgram {

    @Test
    public void testImportFiles(){
        FileReader fileReader = new FileReader("files");
        File[] files =  fileReader.importFiles();
        Assertions.assertEquals(1000,files.length);
    }
    @Test
    public void testReadingAFile(){
        FileReader fileReader = new FileReader("files");
        File[] files = fileReader.importFiles();
        Assertions.assertTrue(fileReader.readDataFromFile(files[0]));
    }
    @Test
    public void testReadingFile2(){
        File file = new File("C:\\Users\\Alireza\\Desktop\\Winter1400-Co_Op\\files\\57111");
        FileReader fileReader = new FileReader("files");
        Assertions.assertFalse(fileReader.readDataFromFile(file));

    }
    @Test
    public void testProcessData(){
        String data = "I am looking for publically accessible sources of data depicting braiand neuron functions.";
        FileReader fileReader = new FileReader("files");
        String[] results = fileReader.processDocumentAndGiveWords(data);
        String[] expected = new String[]{"i","am","look","for","public","access","sourc","of","data","depict","braiand","neuron","function"};
        for(int i = 0; i < expected.length;i++)
           Assertions.assertEquals(expected[i],results[i]);
    }
    @Test
    public void testPuttingWordsInHashMap(){
        FileReader fileReader = new FileReader("files");
        HashMap<String, HashSet<Integer>> result = fileReader.getIndexes();
        List<Integer> list = Arrays.asList(58913,58569,58578,58886,58912,58940,58965,59007,59105,59144,59183);
        HashSet<Integer> expected = new HashSet<>(list);

        Assertions.assertEquals(expected,result.get("remind"));
    }
}
