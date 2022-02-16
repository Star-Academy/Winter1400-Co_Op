
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
    private int beginningIndex, endingIndex, beginningIterator, endingIterator;
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



    private boolean isConsonant(int index)
    {
        return switch (chars[index]) {
            case 'a', 'e', 'i', 'o', 'u' -> false;
            case 'y' -> index == 0 || !isConsonant(index - 1);
            default -> true;
        };
    }

    /** Consonant class measures the number of consonant sequences between 0 and beginningIterator. if c is
       a consonant sequence and v a vowel sequence, and <..> indicates arbitrary
       presence, for example:
          <c><v>       gives 0
          <c>vc<v>     gives 1
          <c>vcvc<v>   gives 2
          ....
    */
    class Consonant{
        int i = 0;
       public boolean checkConsonantForI(boolean doesCheckIsConsonant){
           while(true) {
               if (i > beginningIterator) return true;
               if (doesCheckIsConsonant == isConsonant(i)) break;
               i++;
           }
           i++;
           return false;
       }
       public int returnNumberOfConsonantSequences() {
           int n = 0;
           if (checkConsonantForI(false)) return n;
           while(true) {
               if (checkConsonantForI(true)) return n;
               n++;
               if (checkConsonantForI(false)) return n;
           }
       }
    }


    /** from0ToJAreVowels() is true <=> 0,...beginningIterator contains a vowel */

    private boolean from0ToJAreVowels() {
        for (int i = 0; i <= beginningIterator; i++){
            if (! isConsonant(i)) return true;
        }
        return false;
    }

    /** checkDoubleConsonantSequence(beginningIterator) is true <=> beginningIterator,(beginningIterator-1) contain a double consonant. */
    private boolean areTwoLettersEqual(int index){
        return chars[index] != chars[index-1];
    }
    private boolean checkDoubleConsonantSequence(int index) {
        if (index < 1 || areTwoLettersEqual(index)) return false;
        return isConsonant(index);
    }

   /** checkConsonantVowelConsonantSequence(i) is true <=> i-2,i-1,i has the form consonant - vowel - consonant
      and also if the second c is not w,x or y. this is used when trying to
      restore an e at the end of a short word. e.g.

         cav(e), lov(e), hop(e), crim(e), but
         snow, box, tray.

   */
    private boolean isSequenceWXY(int index){
        int character = chars[index];
        return character != 'w' && character != 'x' && character != 'y';
    }

    private boolean checkConsonantVowelConsonantSequence(int index) {
        if (index < 2 || !isConsonant(index) || isConsonant(index-1) || !isConsonant(index-2)) return false;
        return isSequenceWXY(index);

    }

    private boolean ends(String string) {
        int stringLength = string.length();
        int o = endingIterator - stringLength + 1;
        if (o < 0) return false;
        for (int i = 0; i < stringLength; i++)
            if (chars[o + i] != string.charAt(i))
                return false;
        beginningIterator = endingIterator - stringLength;
        return true;
    }

   /** adjustCharacterFromString(string) sets (beginningIterator + 1),...,endingIterator to
    * the characters in the string s, readjusting endingIterator. */

    private void adjustCharacterFromString(String string) {
        int l = string.length();
        for (int i = 0; i < l; i++)
            chars[i + beginningIterator + 1] = string.charAt(i);
        endingIterator = beginningIterator + l;
    }

    /** callAdjust(s) is used further down. */

    private void callAdjust(String s) {
        if (new Consonant().returnNumberOfConsonantSequences() > 0)
            adjustCharacterFromString(s);
    }

   /** changePlurals() gets rid of plurals and -ed or -ing. e.g.

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
            endingIterator = beginningIterator;
            String[] strings = {"at", "bl", "iz"};
            for (String string : strings) {
                if (ends(string)){
                    adjustCharacterFromString(string + "e");
                    return;
                }
            }
            if (checkDoubleConsonantSequence(endingIterator)) {
                endingIterator--;
                int ch = chars[endingIterator];
                if (ch == 'l' || ch == 's' || ch == 'z') endingIterator++;
            }
            else if (new Consonant().returnNumberOfConsonantSequences() == 1
                    && checkConsonantVowelConsonantSequence(endingIterator)) adjustCharacterFromString("e");
        }
    }

    private void changePlurals() {
        if (chars[endingIterator] == 's') {
            if (ends("sses")) endingIterator -= 2;
            else if (ends("ies")) adjustCharacterFromString("i");
            else if (chars[endingIterator -1] != 's') endingIterator--;
        }
        if (ends("eed")) {
            if (new Consonant().returnNumberOfConsonantSequences() > 0) endingIterator--;
        }
        else changePluralsEndWithEdAndIng();
    }

    /** turnYToI() turns terminal y to i when there is another vowel in the stem. */

    private void turnYToI() {
        if (ends("y") && from0ToJAreVowels()) chars[endingIterator] = 'i';
    }


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
    /** changeDoubleSuffix() maps double suffices to single ones. so -ization ( = -ize plus
     -ation) maps to -ize etc. note that the string before the suffix must give
     Consonant class output > 0. */
    private void changeDoubleSuffix() {
        if (endingIterator == 0) return;
        doubleSuffixesFirstStep(chars[endingIterator -1]);
        doubleSuffixesSecondStep(chars[endingIterator -1]);
        doubleSuffixesThirdStep(chars[endingIterator -1]);
    }

    /** changeSuffixFinish() deals with -ic-, -full, -ness etc. similar strategy to step3. */

    private void changeICAndFullSuffix() {
        switch (chars[endingIterator]) {
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



    private boolean changeSuffixFinishFirstStep(char ch){
        switch (ch) {
            case 'a' -> {
                return ends("al");
            }
            case 'c' -> {
                return ends("ance") || ends("ence");
            }
            case 'e' -> {
                return ends("er");
            }
            case 'i' -> {
                return ends("ic");
            }

        }
        return false;
    }
    private boolean changeSuffixFinishSecondStep(char ch){
        switch (ch){
            case 'l' -> {
                return ends("able") || ends("ible");
            }
            case 'n' -> {
                return ends("ant") || ends("ement") || ends("ment") || ends("ent");
            }
        }
        return false;
    }
    private boolean changeSuffixFinishThirdStep(char ch){
        switch (ch){
            case 'o' -> {
                return (ends("ion") && beginningIterator >= 0
                        && (chars[beginningIterator] == 's' || chars[beginningIterator] == 't')) || ends("ou");
            }
            /* takes care of -ous */
            case 's' -> {
                return ends("ism");
            }
            case 't' -> {
                return ends("ate") || ends("iti");
            }

        }
        return false;
    }
    private boolean changeSuffixFinishFourthStep(char ch){
        switch (ch) {
            case 'u' -> {
                return ends("ous");
            }
            case 'v' -> {
                return ends("ive");
            }
            case 'z' -> {
                return ends("ize");
            }
            default -> {
                return false;
            }
        }
    }
    /** changeSuffixFinish() takes off -ant, -ence etc., in context <c>vcvc<v>. */
    private void changeSuffixFinish() {
        if (endingIterator == 0) return;
        char ch = chars[endingIterator -1];
        boolean b = changeSuffixFinishFirstStep(ch) || changeSuffixFinishSecondStep(ch)
                || changeSuffixFinishThirdStep(ch) || changeSuffixFinishFourthStep(ch);
        if (b && (new Consonant().returnNumberOfConsonantSequences() > 1)) endingIterator = beginningIterator;
    }

    /** removeFinalE() removes a final -e if Consonant class output > 1. */
    private void removeFinalE() {
        beginningIterator = endingIterator;
        if (chars[endingIterator] == 'e') {
            int a = new Consonant().returnNumberOfConsonantSequences();
            if (a > 1 || a == 1 && !checkConsonantVowelConsonantSequence(endingIterator -1)) endingIterator--;
        }
        if (chars[endingIterator] == 'l' && checkDoubleConsonantSequence(endingIterator)
                && new Consonant().returnNumberOfConsonantSequences() > 1) endingIterator--;
    }

    /** Stem the word placed into the Stemmer buffer through calls to add().
     * Returns true if the stemming process resulted in a word different
     * from the input.  You can retrieve the result with
     * getResultLength()/getResultBuffer() or toString().
     */
    public void stem() {
        endingIterator = beginningIndex - 1;
        if (endingIterator > 1) {
            changePlurals();
            turnYToI();
            changeDoubleSuffix();
            changeICAndFullSuffix();
            changeSuffixFinish();
            removeFinalE();
        }
        endingIndex = endingIterator + 1;
        beginningIndex = 0;
    }


    /** stemWord gets a string and stems it to the output */
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
