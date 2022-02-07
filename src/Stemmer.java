
/*
   Porter stemmer in Java.
   Source : http://www.tartarus.org/~martin/PorterStemmer
*/


/**
 * Stemmer, implementing the Porter Stemming Algorithm
 *
 * The Stemmer class transforms a word into its root form.  The input
 * word can be provided a character at time (by calling add()), or at once
 * by calling one of the various stem(something) methods.
 */

class Stemmer {
    private char[] chars;
    private int beginningIndex, endingIndex, beginningIterator, endingItrator;
    public Stemmer() {
        chars = new char[50];
        beginningIndex = 0;
        endingIndex = 0;
    }

    /**
     * Add a character to the word being stemmed.  When you are finished
     * adding characters, you can call stem(void) to stem the word.
     */

    public void add(char character) {
        if (beginningIndex == chars.length) {
            char[] newChars = new char[beginningIndex + 50];
            System.arraycopy(chars, 0, newChars, 0, beginningIndex);
            chars = newChars;
        }
        chars[beginningIndex++] = character;
    }


    /**
     * After a word has been stemmed, it can be retrieved by toString(),
     * or a reference to the internal buffer can be retrieved by getResultBuffer
     * and getResultLength (which is generally more efficient.)
     */
    public String toString() { return new String(chars,0, endingIndex); }

    /**
     * Returns the length of the word resulting from the stemming process.
     */
    public int getResultLength() { return endingIndex; }

    /**
     * Returns a reference to a character buffer containing the results of
     * the stemming process.  You also need to consult getResultLength()
     * to determine the length of the result.
     */
    public char[] getResultBuffer() { return chars; }


    private boolean isConsonant(int index)
    {
        return switch (chars[index]) {
            case 'a', 'e', 'i', 'o', 'u' -> false;
            case 'y' -> index == 0 || !isConsonant(index - 1);
            default -> true;
        };
    }

   /* m() measures the number of consonant sequences between 0 and j. if c is
      a consonant sequence and v a vowel sequence, and <..> indicates arbitrary
      presence,

         <c><v>       gives 0
         <c>vc<v>     gives 1
         <c>vcvc<v>   gives 2
         <c>vcvcvc<v> gives 3
         ....
   */
    private int checkConsonantForI(int i, int n, boolean doesCheckIsConsonant){
        while(true) {
            if (i > beginningIterator) return n;
            if(doesCheckIsConsonant){
                if (isConsonant(i)) break;
            } else {
                if (!isConsonant(i)) break;
            }
            i++;
        }
        i++;
        return i;
    }
    private int returnNumberOfConsonantSequences() {
        int n = 0;
        int i = 0;
        i = checkConsonantForI(i, n, false);
        while(true) {
            i = checkConsonantForI(i, n, true);
            n++;
            i = checkConsonantForI(i, n, false);
        }
    }

    /* from0ToJAreVowels() is true <=> 0,...j contains a vowel */

    private boolean from0ToJAreVowels() {
        for (int i = 0; i <= beginningIterator; i++){
            if (! isConsonant(i)) return true;
        }
        return false;
    }

    /* checkDoubleConsonantSequence(j) is true <=> j,(j-1) contain a double consonant. */

    private boolean checkDoubleConsonantSequence(int index) {
        if (index < 1) return false;
        if (chars[index] != chars[index-1]) return false;
        return isConsonant(index);
    }

   /* checkConsonantVowelConsonantSequence(i) is true <=> i-2,i-1,i has the form consonant - vowel - consonant
      and also if the second c is not w,x or y. this is used when trying to
      restore an e at the end of a short word. e.g.

         cav(e), lov(e), hop(e), crim(e), but
         snow, box, tray.

   */

    private boolean checkConsonantVowelConsonantSequence(int index) {
        if (index < 2 || !isConsonant(index) || isConsonant(index-1) || !isConsonant(index-2)) return false;
        int character = chars[index];
        return character != 'w' && character != 'x' && character != 'y';

    }

    private boolean ends(String string) {
        int stringLength = string.length();
        int o = endingItrator - stringLength + 1;
        if (o < 0) return false;
        for (int i = 0; i < stringLength; i++)
            if (chars[o + i] != string.charAt(i))
                return false;
        beginningIterator = endingItrator -stringLength;
        return true;
    }

   /* adjustCharacterFromString(string) sets (j+1),...k to the characters in the string s, readjusting
      k. */

    private void adjustCharacterFromString(String string) {
        int l = string.length();
        for (int i = 0; i < l; i++)
            chars[i + beginningIterator + 1] = string.charAt(i);
        endingItrator = beginningIterator + l;
    }

    /* r(s) is used further down. */

    private void callAdjust(String s) {
        if (returnNumberOfConsonantSequences() > 0)
            adjustCharacterFromString(s);
    }

   /* changePlurals() gets rid of plurals and -ed or -ing. e.g.

          caresses  ->  caress
          ponies    ->  poni
          ties      ->  ti
          caress    ->  caress
          cats      ->  cat

          feed      ->  feed
          agreed    ->  agree
          disabled  ->  disable

          matting   ->  mat
          mating    ->  mate
          meeting   ->  meet
          milling   ->  mill
          messing   ->  mess

          meetings  ->  meet

   */
    private void changePluralsEndWithEdAndIng(){
        if ((ends("ed") || ends("ing")) && from0ToJAreVowels()) {
            endingItrator = beginningIterator;
            if (ends("at")) adjustCharacterFromString("ate");
            else if (ends("bl")) adjustCharacterFromString("ble");
            else if (ends("iz")) adjustCharacterFromString("ize");
            else if (checkDoubleConsonantSequence(endingItrator)) {
                endingItrator--;
                int ch = chars[endingItrator];
                if (ch == 'l' || ch == 's' || ch == 'z') endingItrator++;
            }
            else if (returnNumberOfConsonantSequences() == 1
                    && checkConsonantVowelConsonantSequence(endingItrator)) adjustCharacterFromString("e");
        }
    }

    private void changePlurals() {
        if (chars[endingItrator] == 's') {
            if (ends("sses")) endingItrator -= 2;
            else if (ends("ies")) adjustCharacterFromString("i");
            else if (chars[endingItrator -1] != 's') endingItrator--;
        }
        if (ends("eed")) {
            if (returnNumberOfConsonantSequences() > 0) endingItrator--;
        }
        else changePluralsEndWithEdAndIng();
    }

    /* step2() turns terminal y to i when there is another vowel in the stem. */

    private void turnYToI() {
        if (ends("y") && from0ToJAreVowels()) chars[endingItrator] = 'i';
    }

   /* changeDoubleSuffix() maps double suffices to single ones. so -ization ( = -ize plus
      -ation) maps to -ize etc. note that the string before the suffix must give
      m() > 0. */
    private void doubleSuffixesFirstStep(char ch){
        switch (ch){
            case 'a'-> {if (ends("ational")) { callAdjust("ate"); break; }
                if (ends("tional")) callAdjust("tion");
            }
            case 'c'-> {if (ends("enci")) { callAdjust("ence"); break; }
                if (ends("anci")) callAdjust("ance");
            }
            case 'e' -> {if (ends("izer")) callAdjust("ize"); }

        }
    }
    private void doubleSuffixesSecondStep(char ch){
        switch (ch){
            case 'l' -> {if (ends("bli")) { callAdjust("ble"); break; }
                if (ends("alli")) { callAdjust("al"); break; }
                if (ends("entli")) { callAdjust("ent"); break; }
                if (ends("eli")) { callAdjust("e"); break; }
                if (ends("ousli")) callAdjust("ous");
            }
            case 'o' -> {if (ends("ization")) { callAdjust("ize"); break; }
                if (ends("ation")) { callAdjust("ate"); break; }
                if (ends("ator")) callAdjust("ate");
            }

        }
    }
    private void doubleSuffixesThirdStep(char ch){
        switch (ch) {
            case 's' -> {if (ends("alism")) { callAdjust("al"); break; }
                if (ends("iveness")) { callAdjust("ive"); break; }
                if (ends("fulness")) { callAdjust("ful"); break; }
                if (ends("ousness")) callAdjust("ous");
            }
            case 't' -> {if (ends("aliti")) { callAdjust("al"); break; }
                if (ends("iviti")) { callAdjust("ive"); break; }
                if (ends("biliti")) callAdjust("ble");
            }
            case 'g' -> {if (ends("logi")) callAdjust("log");}
        }
    }
    private void changeDoubleSuffix() {
        if (endingItrator == 0) return;
        doubleSuffixesFirstStep(chars[endingItrator -1]);
        doubleSuffixesSecondStep(chars[endingItrator -1]);
        doubleSuffixesThirdStep(chars[endingItrator -1]);
    }

    /* changeSuffixFinish() deals with -ic-, -full, -ness etc. similar strategy to step3. */

    private void changeICAndFullSuffix() {
        switch (chars[endingItrator]) {
            case 'e' -> {if (ends("icate")) { callAdjust("ic"); break; }
                if (ends("ative")) { callAdjust(""); break; }
                if (ends("alize")) callAdjust("al");
            }
            case 'i' -> {if (ends("iciti")) callAdjust("ic"); }
            case 'l' -> {if (ends("ical")) { callAdjust("ic"); break; }
                if (ends("ful")) callAdjust("");
            }
            case 's' -> {if (ends("ness")) callAdjust(""); }
        }
    }

    /* step5() takes off -ant, -ence etc., in context <c>vcvc<v>. */

    private boolean changeSuffixFinishFirstStep(char ch){
        switch (ch) {
            case 'a' -> {
                ends("al");
                return true;
            }
            case 'c' -> {
                if (ends("ance")) break;
                ends("ence");
                return true;
            }
            case 'e' -> {
                ends("er");
                return true;
            }
            case 'i' -> {
                ends("ic");
                return true;
            }

        }
        return false;
    }
    private boolean changeSuffixFinishSecondStep(char ch){
        switch (ch){
            case 'l' -> {
                if (ends("able")) break;
                ends("ible");
                return true;
            }
            case 'n' -> {
                if (ends("ant")) break;
                if (ends("ement")) break;
                if (ends("ment")) break;
                ends("ent");
                return true;
            }
        }
        return false;
    }
    private boolean changeSuffixFinishThirdStep(char ch){
        switch (ch){
            case 'o' -> {
                if (ends("ion") && beginningIterator >= 0
                        && (chars[beginningIterator] == 's' || chars[beginningIterator] == 't')) break;
                if (ends("ou")) break;
                return true;
            }
            /* takes care of -ous */
            case 's' -> {
                if (ends("ism")) break;
                return true;
            }
            case 't' -> {
                if (ends("ate")) break;
                if (ends("iti")) break;
                return true;
            }

        }
        return false;
    }
    private boolean changeSuffixFinishFourthStep(char ch){
        switch (ch) {
            case 'u' -> {
                if (ends("ous")) break;
                return true;
            }
            case 'v' -> {
                if (ends("ive")) break;
                return true;
            }
            case 'z' -> {
                if (ends("ize")) break;
                return true;
            }
            default -> {
                return true;
            }
        }
        return false;
    }
    private void changeSuffixFinish() {
        if (endingItrator == 0) return;
        char ch = chars[endingItrator -1];
        boolean b = changeSuffixFinishFirstStep(ch) && changeSuffixFinishSecondStep(ch)
                && changeSuffixFinishThirdStep(ch) && changeSuffixFinishFourthStep(ch);
        if (!b && (returnNumberOfConsonantSequences() > 1)) endingItrator = beginningIterator;
    }

    /* step6() removes a final -e if m() > 1. */

    private void removeFinalE() {
        beginningIterator = endingItrator;
        if (chars[endingItrator] == 'e') {
            int a = returnNumberOfConsonantSequences();
            if (a > 1 || a == 1 && !checkConsonantVowelConsonantSequence(endingItrator -1)) endingItrator--;
        }
        if (chars[endingItrator] == 'l' && checkDoubleConsonantSequence(endingItrator)
                && returnNumberOfConsonantSequences() > 1) endingItrator--;
    }

    /** Stem the word placed into the Stemmer buffer through calls to add().
     * Returns true if the stemming process resulted in a word different
     * from the input.  You can retrieve the result with
     * getResultLength()/getResultBuffer() or toString().
     */
    public void stem() {
        endingItrator = beginningIndex - 1;
        if (endingItrator > 1) {
            changePlurals();
            turnYToI();
            changeDoubleSuffix();
            changeICAndFullSuffix();
            changeSuffixFinish();
            removeFinalE();
        }
        endingIndex = endingItrator + 1;
        beginningIndex = 0;
    }


    public String stemWord(String string)
    {
        string = string.toLowerCase();
        char[] chars = string.toCharArray();
        for (char ch : chars) {
            if (Character.isLetter(ch)) {
                add(ch);
            }
        }
        stem();
        return toString();

    }
}
