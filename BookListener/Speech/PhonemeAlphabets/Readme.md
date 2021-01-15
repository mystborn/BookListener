# Phoneme Alphabets

## This Project

This folder contains some phoneme alphabets that can be deserialized into a PhonemeAlphabet object with an XmlSerializer.

## Alphabets Supported Out of the Box

These alphabets are supported out of the box (They are static properties on PhonemeAlphabet). Other alphabets can be used as well, but they are not defined in this library.

* American English (UPS).

## What is a Phoneme Alphabet

A phoneme alphabet is a collection of phonemes that represent consonants, vowels, and prosodies (basically other information used to build the sound of a word, like syllable separators and tone information).

Some common alphabets are:
* International Phonetic Alphabet (IPA)
* Microsoft's Universal Phone Set (UPS)
* Speech API Phone Set (SAPI)

IPA is what is typically used in dictionaries when describing a words pronunciation. There are two versions of it, one that is represented with ASCII characters and one that is represented with UTF characters. (Note that the SpeechSynthesizer class only allows the use of the utf version).

UPS was created by Microsoft to be a machine-readable phonetic alphabet. It only uses ASCII characters, and is what will be used throughout most of the documentation.

SAPI is a legacy phonetic alphabet created by Microsoft. It has minimal language support and has been deprecated in favor of UPS.

## Phoneme Alphabet Information

Finding information on phoneme alphabets is surprisingly difficult I find. For example, I couldn't find an easy to use UTF IPA table anywhere. By far the most encompassing resource is the documentation for the [Microsoft Speech Platform SDK 11](https://documentation.help/Microsoft-Speech-Platform-SDK-11/). Some important pages from there include:
* [Phonetic Alphabet Reference](https://documentation.help/Microsoft-Speech-Platform-SDK-11/ccbe7d0a-2aa5-4987-98c5-b65089bac8c3.htm) - Contains links to tables that describe the all of the UPS characters.
* [American English Phoneme Representation](https://documentation.help/Microsoft-Speech-Platform-SDK-11/English_Phoneme.htm) - A table that contains all of the phonemes used in American English
* [prosody Element](https://documentation.help/Microsoft-Speech-Platform-SDK-11/6c16338c-2b5a-4847-aa60-24792d24aff6.htm) - Describes how to use prosody elements in Speech Synthesis Markup Language (SSML) files.

While that documentation might contain a little bit of everything, it's a bit bloated and unorganized. Some other useful pages include:
* [Subsets of UPS for specific languages](https://docs.microsoft.com/en-us/previous-versions/office/developer/communication-server-2007/bb813107(v=office.12)#phoneme-tables) - These are the easiest tables to use by far.