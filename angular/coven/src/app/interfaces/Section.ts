export type SectionContent = any; // Change this to a more specific type if you know what the nested arrays contain

export interface Sections {
  displaySidebar: SectionContent[][];
  folderId: SectionContent[][];
  firstname: SectionContent[][];
  lastname: SectionContent[][];
  nickname: SectionContent[][];
  dobDisplay: SectionContent[][];
  age: SectionContent[][];
  height: SectionContent[][];
  birthplace: SectionContent[][];
  weight: SectionContent[][];
  gender: SectionContent[][];
  speciesDisplay: SectionContent[][];
  physique: SectionContent[][];
  identifyingCharacteristics: SectionContent[][];
  quirksPhysical: SectionContent[][];
  clothing: SectionContent[][];
  rpgAlignment: SectionContent[][];
  employment: SectionContent[][];
  mentalTraumas: SectionContent[][];
  sexuality: SectionContent[][];
  languages: SectionContent[][];
  quirksPersonality: SectionContent[][];
  hygiene: SectionContent[][];
  family: SectionContent[][];
  titles: SectionContent[][];
  socialAptitude: SectionContent[][];
}