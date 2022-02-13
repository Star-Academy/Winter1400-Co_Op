import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

public class StemmerTest {
    public Stemmer stemmer;
    @BeforeEach
    public void stemmerInit(){
        stemmer = new Stemmer();
    }
    @Test
    public void test(){
        Assertions.assertEquals("optim", stemmer.stemWord("optimization"));
    }
    @Test
    public void endingWithEdTest(){
        Assertions.assertEquals("work", stemmer.stemWord("worked"));
    }
    @Test
    public void endingWithConsTest(){
        Assertions.assertEquals("debug", stemmer.stemWord("debugging"));
    }
    @Test
    public void pluralWithSTest(){
        Assertions.assertEquals("car", stemmer.stemWord("cars"));
    }
    @Test
    public void endingWithAbleTest(){
        Assertions.assertEquals("compat", stemmer.stemWord("compatible"));
    }
    @Test
    public void endingWithIzationTest(){
        Assertions.assertEquals("final", stemmer.stemWord("finalization"));
    }
    @Test
    public void endingWithAtionAndAtorTest(){
        Assertions.assertEquals("alloc", stemmer.stemWord("allocation"));
        Assertions.assertEquals("motiv", stemmer.stemWord("motivator"));
    }
    @Test
    public void endingWithFulnessTest(){
        Assertions.assertEquals("aw", stemmer.stemWord("awfulness"));
    }
    @Test
    public void endingWithETest(){
        Assertions.assertEquals("ablaz", stemmer.stemWord("ablaze"));
    }

}
