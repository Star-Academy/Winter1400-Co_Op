public class DocumentProcessor {

    private static final Stemmer stemmer;
    private String data;

    static {
        stemmer = new Stemmer();
    }


    public DocumentProcessor(String data) {
        this.data = data;
    }


    private void toLowerCase() {
        if (data == null) return;
        data = data.toLowerCase();
    }

    private void removeSigns() {
        if (data == null) return;
        removeDashes();
        removeNonAlphaNumerics();
    }

    private void removeDashes() {
        data = data.replaceAll("[-?]+", " ");
    }

    private void removeNonAlphaNumerics() {
        data = data.replaceAll("[^a-zA-Z0-9\\s]+", " ");
    }

    private String[] toStemmedSplit() {
        if (data == null) return new String[0];
        String[] splitData = splitData();
        return stemSplitData(splitData);

    }

    private String[] stemSplitData(String[] splitData) {
        for (int i = 0; i < splitData.length; i++) {
            splitData[i] = stemmer.stemWord(splitData[i]);
        }
        return splitData;
    }

    private String[] splitData() {
        return data.split("\\s+");
    }

    public String[] getNormalizedWords() {
        toLowerCase();
        removeSigns();
        return toStemmedSplit();
    }

}