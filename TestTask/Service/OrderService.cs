using Microsoft.EntityFrameworkCore;
using TestTask.Entity;
using TestTask.IService;

namespace TestTask.Service
{
    /// <summary>
    ///     Сервис для управления заказами.
    ///     <param name="db">Контекст базы данных.</param>
    ///     <param name="logger">Логгер.</param>
    /// </summary>
    public class OrderService : IOrderService
    {
        private ApplicationContext db;
        private ILogger<OrderService> Logger;
        public OrderService(ApplicationContext db, ILogger<OrderService> logger)
        {
            this.db = db;
            Logger = logger;
        }

        /// <summary>
        ///     Создает новый заказ и привязывает его к пользователю.
        ///     <param name="order">Объект заказа.</param>
        /// </summary>
        public async Task Create(Order order)
        {
            try
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }

        /// <summary>
        ///     Удаляет заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Сообщение о результате операции.</returns>
        public async Task<string> Delete(string id)
        {
            try
            {
                var result = await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if(result != null)
                {
                    db.Orders.Remove(result);
                    await db.SaveChangesAsync();
                    return "Заказ успешно удален";
                }
                return "Заказ не найден";
            }
            catch (Exception ex)
            {
                Logger.LogError($"{ex.Message}");
                return "Ошибка при удалении";
            }
        }

        /// <summary>
        ///     Получает список заказов пользователя.
        /// </summary>
        /// <returns>Список заказов</returns>
        public async Task<List<Order>> GetAll()
        {
            try
            {
                return await db.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"{ex.Message}");
                return new List<Order>();
            }
        }
    }
}
