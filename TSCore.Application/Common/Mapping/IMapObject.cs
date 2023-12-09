using AutoMapper;

namespace TSCore.Application.Common.Mapping;

public interface IMapObject<T>
{
    void Mapping(Profile profile);
}