namespace SampleLibrary;

public class Stemmer
{
    private char[] wordArray;		// character array copy of the given string
	private int _stem, _end;			// indices to the current end (last letter) of the stem and the word in the array

	// Get the stem of a word at least three letters long:
	public string StemWord ( string word )
	{
		if ( string.IsNullOrWhiteSpace ( word ) || word.Length < 3 )
			return word;

		wordArray = word.ToCharArray ();
		_stem = 0;
		_end = word.Length - 1;

		CorrectPlurals ();
		ChangeLastYToI ();
		FoldDoubleSuffixes ();
		CorrectFullAndNess ();
		ChangeSuffixFinish ();
		RemoveFinalE ();

		return new string ( wordArray, 0, _end + 1 );
	}

	private void CorrectEndingWithEdAndIng()
	{
		_end = _stem;
		if ( EndsWith ( "at" ) )
			OverwriteEnding ( "ate" );
		else if ( EndsWith ( "bl" ) )
			OverwriteEnding ( "ble" );
		else if ( EndsWith ( "iz" ) )
			OverwriteEnding ( "ize" );
		else if ( EndsWithDoubleConsonant () )
		{
			if ( !"lsz".Contains ( wordArray [ _end - 1 ] ) )
				Truncate ();
		}
		else if ( ConsonantSequenceCount () == 1 && PrecededByCvc ( _end ) )
			OverwriteEnding ( "e" );
	}

	// Step 1: remove basic plurals and -ed/-ing:
	private void CorrectPlurals ()
	{
		if ( wordArray [ _end ] == 's' )
		{
			if ( EndsWith ( "sses" ) )
				Truncate ( 2 );
			else if ( EndsWith ( "ies" ) )
				OverwriteEnding ( "i" );
			else if ( wordArray [ _end - 1 ] != 's' )
				Truncate ();
		}

		if ( EndsWith ( "eed" ) )
		{
			if ( ConsonantSequenceCount () > 0 )
				Truncate ();
		}
		else if ( ( EndsWith ( "ed" ) || EndsWith ( "ing" ) ) && VowelInStem () )
		{
			CorrectEndingWithEdAndIng();
		}
	}

	// Step 2: change a terminal 'y' to 'i' if there is another vowel in the stem:
	private void ChangeLastYToI ()
	{
		if ( EndsWith ( "y" ) && VowelInStem () )
			OverwriteEnding ( "i" );
	}

	private void FoldDoubleSuffixesFirstStep(char ch)
	{
		switch (ch)
		{
			case 'a':
				if ( ReplaceEnding ( "ational", "ate" ) ) break;
				ReplaceEnding ( "tional", "tion" ); break;
			case 'c':
				if ( ReplaceEnding ( "enci", "ence" ) ) break;
				ReplaceEnding ( "anci", "ance" ); break;
			case 'e':
				ReplaceEnding ( "izer", "ize" ); break;
			case 'l':
				if ( ReplaceEnding ( "bli", "ble" ) ) break;
				if ( ReplaceEnding ( "alli", "al" ) ) break;
				if ( ReplaceEnding ( "entli", "ent" ) ) break;
				if ( ReplaceEnding ( "eli", "e" ) ) break;
				ReplaceEnding ( "ousli", "ous" ); break;
		}
	}

	private void FoldDoubleSuffixesSecondStep(char ch)
	{
		switch (ch)
		{
			case 'o':
				if ( ReplaceEnding ( "ization", "ize" ) ) break;
				if ( ReplaceEnding ( "ation", "ate" ) ) break;
				ReplaceEnding ( "ator", "ate" ); break;
			case 's':
				if ( ReplaceEnding ( "alism", "al" ) ) break;
				if ( ReplaceEnding ( "iveness", "ive" ) ) break;
				if ( ReplaceEnding ( "fulness", "ful" ) ) break;
				ReplaceEnding ( "ousness", "ous" ); break;
			case 't':
				if ( ReplaceEnding ( "aliti", "al" ) ) break;
				if ( ReplaceEnding ( "iviti", "ive" ) ) break;
				ReplaceEnding ( "biliti", "ble" ); break;
			case 'g':
				ReplaceEnding ( "logi", "log" ); break;
		}
	}

	// Step 3: fold double suffixes to single suffix, e.g., -ization = -ize + -ation -> -ize:
	private void FoldDoubleSuffixes ()
	{
		FoldDoubleSuffixesFirstStep(wordArray[_end - 1]);
		FoldDoubleSuffixesSecondStep(wordArray[_end - 1]);
	}

	// Step 4: replace -ic-, -full, -ness, etc. with simpler endings:
	private void CorrectFullAndNess ()
	{
		switch ( wordArray [ _end ] )
		{
			case 'e':
				if ( ReplaceEnding ( "icate", "ic" ) ) break;
				if ( ReplaceEnding ( "ative", "" ) ) break;
				ReplaceEnding ( "alize", "al" ); break;
			case 'i':
				ReplaceEnding ( "iciti", "ic" ); break;
			case 'l':
				if ( ReplaceEnding ( "ical", "ic" ) ) break;
				ReplaceEnding ( "ful", "" ); break;
			case 's':
				ReplaceEnding ( "ness", "" ); break;
		}
	}
	
	private bool changeSuffixFinishFirstStep(char ch){
        switch (ch) {
            case 'a' : {
                return EndsWith("al");
            }
            case 'c' : {
                return EndsWith("ance") || EndsWith("ence");
            }
            case 'e' : {
                return EndsWith("er");
            }
            case 'i' : {
                return EndsWith("ic");
            }

        }
        return false;
    }
    private bool changeSuffixFinishSecondStep(char ch){
        switch (ch){
            case 'l' : {
                return EndsWith("able") || EndsWith("ible");
            }
            case 'n' : {
                return EndsWith("ant") || EndsWith("ement") || EndsWith("ment") || EndsWith("ent");
            }
        }
        return false;
    }
    private bool changeSuffixFinishThirdStep(char ch){
        switch (ch){
            case 'o' : {
                return (EndsWith("ion") && _stem >= 0 && ( wordArray[_stem] == 's' || wordArray[_stem] == 't' )) || EndsWith("ou");
            }
            /* takes care of -ous */
            case 's' : {
                return EndsWith("ism");
            }
            case 't' : {
                return EndsWith("ate") || EndsWith("iti");
            }

        }
        return false;
    }
    private bool changeSuffixFinishFourthStep(char ch){
        switch (ch) {
            case 'u' : {
                return EndsWith("ous");
            }
            case 'v' : {
                return EndsWith("ive");
            }
            case 'z' : {
                return EndsWith("ize");
            }
            default : {
                return false;
            }
        }
    }
	// Step 5: remove -ant, -ence, etc.:
    private void ChangeSuffixFinish() {
        char ch = wordArray [ _end - 1 ];
        bool b = changeSuffixFinishFirstStep(ch) || changeSuffixFinishSecondStep(ch)
                || changeSuffixFinishThirdStep(ch) || changeSuffixFinishFourthStep(ch);
        if (b && ConsonantSequenceCount () > 1)
	        _end = _stem;
    }

    // Step 6: remove final 'e' if necessary:
	private void RemoveFinalE ()
	{
		_stem = _end;
		if ( wordArray [ _end ] == 'e' )
		{
			var m = ConsonantSequenceCount ();
			if ( m > 1 || m == 1 && !PrecededByCvc ( _end - 1 ) )
				Truncate ();
		}

		if ( wordArray [ _end ] == 'l' && EndsWithDoubleConsonant() && ConsonantSequenceCount () > 1 )
			Truncate ();
	}

	private void Truncate ( int n = 1 )
	{
		_end -= n;
	}
	
	// Count the number of CVC sequences:
	private int ConsonantSequenceCount ()
	{
		int m = 0, index = 0;
		for ( ; index <= _stem && IsConsonant ( index ); index++ ) ;
		if ( index > _stem )
			return 0;

		for ( index++; ; index++ )
		{
			for ( ; index <= _stem && !IsConsonant ( index ); index++ ) ;
			if ( index > _stem )
				return m;

			for ( index++, m++; index <= _stem && IsConsonant ( index ); index++ ) ;
			if ( index > _stem )
				return m;
		}
	}

	// Return true if there is a vowel in the current stem:
	private bool VowelInStem ()
	{
		for ( var i = 0; i <= _stem; i++ )
			if ( !IsConsonant ( i ) )
				return true;
		return false;
	}

	// Returns true if the character at the specified index is a consonant, with special handling for 'y':
	private bool IsConsonant ( int index )
	{
		if ( "aeiou".Contains ( wordArray [ index ] ) )
			return false;

		return wordArray [ index ] != 'y' || index == 0 || !IsConsonant ( index - 1 );
	}

	// Return true if the char. at the current index and the one preceeding it are the same consonant:
	private bool EndsWithDoubleConsonant()
	{
		return _end > 0 && wordArray [ _end ] == wordArray [ _end - 1 ] && IsConsonant ( _end );
	}

	// Check if the letters at i-2, i-1, i have the pattern: consonant-vowel-consonant (CVC) and the second consonant
	// is not w, x or y; used when restoring an 'e' at the end of a short word, e.g., cav(e), lov(e), hop(e), etc.:
	private bool PrecededByCvc ( int index )
	{
		if ( index < 2 || !IsConsonant ( index ) || IsConsonant ( index - 1 ) || !IsConsonant ( index - 2 ) )
			return false;

		return !"wxy".Contains ( wordArray [ index ] );
	}

	// Check if the given string appears at the end of the word:
	private bool EndsWith ( string s )
	{
		int length = s.Length, index = _end - length + 1;
		if ( index >= 0 )
		{
			for ( var i = 0; i < length; i++ )
				if ( wordArray [ index + i ] != s[i] )
					return false;

			_stem = _end - length;
			return true;
		}
		return false;
	}

	// Conditionally replace the end of the word:
	private bool ReplaceEnding ( string suffix, string s )
	{
		if ( EndsWith ( suffix ) && ConsonantSequenceCount () > 0 )
		{
			OverwriteEnding ( s );
			return true;
		}
		return false;
	}

	// Change the end of the word to a given string:
	private void OverwriteEnding ( string s )
	{
		int length = s.Length, index = _stem + 1;
		for ( var i = 0; i < length; i++ )
			wordArray [ index + i ] = s[i];
		_end = _stem + length;
	}
}