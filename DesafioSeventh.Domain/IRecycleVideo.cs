using DesafioSeventh.Domain.ViewModel;

namespace DesafioSeventh.Domain
{
	public interface IRecycleVideoDomain
	{
		void Recycle(int days);
		RecycleStatus Status { get; }
	}
}
