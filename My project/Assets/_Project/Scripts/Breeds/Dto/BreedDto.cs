using System;

namespace _Project.Scripts.Breeds.Dto
{
    [Serializable]
    public class BreedListResponseDto
    {
        public BreedDto[] data;
    }

    [Serializable]
    public class BreedResponseDto
    {
        public BreedDto data;
    }

    [Serializable]
    public class BreedDto
    {
        public string id;
        public BreedAttributesDto attributes;
    }

    [Serializable]
    public class BreedAttributesDto
    {
        public string name;
        public string description;
    }
}
