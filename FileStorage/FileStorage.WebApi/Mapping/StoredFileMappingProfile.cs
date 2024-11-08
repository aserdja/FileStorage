using Amazon.SecretsManager.Model;
using AutoMapper;
using FileStorage.DAL.Models;
using FileStorage.WebApi.DTOs;

namespace FileStorage.WebApi.Mapping
{
	public class StoredFileMappingProfile : Profile
	{
		public StoredFileMappingProfile()
		{
			CreateMap<StoredFile, StoredFileDetailsDTO>()
				.ForMember(dest => dest.StoredFileId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.FileType, opt => opt.MapFrom(src => src.Type))
				.ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.Path));

			CreateMap<StoredFileDetails, StoredFileDetailsDTO>()
				.ForMember(dest => dest.StoredFileDetailsId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.UploadDateTime, opt => opt.MapFrom(src => src.UploadDateTime))
				.ForMember(dest => dest.ExpireDateTime, opt => opt.MapFrom(src => src.ExpireDateTime));
		}
	}
}
