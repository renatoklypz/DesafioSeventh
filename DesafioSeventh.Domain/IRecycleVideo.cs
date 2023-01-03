using DesafioSeventh.Domain.ViewModel;

namespace DesafioSeventh.Domain
{
	public interface IRecycleVideo
	{
		void Recycle(int days);
		RecycleStatus Status { get; }
	}
}
