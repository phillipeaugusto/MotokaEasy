using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MotokaEasy.Core.Infrastructure.Repositories.Contracts;
using Xunit;
using static System.Threading.Tasks.Task;

namespace MotokaEasy.Tests.UnitaryTests.Repositories;

public abstract class RepositoryBaseTests<TEntity, TDtoOutPutModel>
{
  private readonly Mock<IRepository<TEntity, TDtoOutPutModel>> _mockRepository = new();
  private readonly CancellationToken _cancellationToken = new();
  private readonly TEntity _dataBaseInput;
  private readonly TDtoOutPutModel _dataBaseOutPut;
  private readonly List<TEntity> _listInput;
  private readonly Guid _guidFilter;

  public RepositoryBaseTests(TEntity dataBaseInput, TDtoOutPutModel dataBaseOutput, List<TEntity> listInput, Guid guidFilter, List<TDtoOutPutModel> listOutPut)
  {
    _dataBaseInput = dataBaseInput;
    _dataBaseOutPut = dataBaseOutput;
    _listInput = listInput;
    _guidFilter = guidFilter;
    _mockRepository.Setup(x => x.CreateAsync(It.IsAny<TEntity>(), _cancellationToken));
    _mockRepository.Setup(x => x.UpdateAsync(It.IsAny<TEntity>(), _cancellationToken));
    _mockRepository.Setup(x => x.DeleteAsync(It.IsAny<TEntity>(), _cancellationToken));

    _mockRepository.Setup(x => x.UpdateListAsync(It.IsAny<List<TEntity>>(), _cancellationToken));
    _mockRepository.Setup(x => x.DeleteListAsync(It.IsAny<List<TEntity>>(), _cancellationToken));
    _mockRepository.Setup(x => x.CreateListAsync(It.IsAny<List<TEntity>>(), _cancellationToken));

    _mockRepository.Setup(x => x.Create(It.IsAny<TEntity>()));
    _mockRepository.Setup(x => x.Delete(It.IsAny<TEntity>()));
    _mockRepository.Setup(x => x.Exists(It.IsAny<TEntity>())).Returns(_dataBaseInput);
    _mockRepository.Setup(x => x.Update(It.IsAny<TEntity>()));
    
    _mockRepository.Setup(x => x.ExistsAsync(It.IsAny<TEntity>(), _cancellationToken)).Returns(FromResult(_dataBaseInput));
    _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), _cancellationToken)).Returns(FromResult(_dataBaseOutPut));
    _mockRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), _cancellationToken)).Returns(FromResult(_dataBaseInput));
    _mockRepository.Setup(x => x.GetAllByOutPutAsync(_cancellationToken)).Returns(FromResult(listOutPut));
    _mockRepository.Setup(x => x.GetAllAsync(_cancellationToken)).Returns(FromResult(_listInput));
  }

  [Fact]
  public async Task teste_obtencao_entidade_por_id_assincrona()
  {
    var obj = await _mockRepository.Object.GetByIdAsync(_guidFilter, _cancellationToken);
    Assert.Equal(obj, _dataBaseOutPut);
  }
  
  [Fact]
  public async Task teste_existencia_entidade_assincrona()
  {
    var obj = await _mockRepository.Object.ExistsAsync(_dataBaseInput, _cancellationToken);
    Assert.Equal(obj, _dataBaseInput);
  }
  
  [Fact]
  public async Task teste_obtencao_completa_entidade_assincrona()
  {
    var obj = await _mockRepository.Object.GetAllAsync(_cancellationToken);
    Assert.Equal(_listInput, obj);
  }

  [Fact]
  public void teste_criacao_entidade()
  {
     _mockRepository.Object.Create(_dataBaseInput);
  }

  [Fact]
  public void teste_remocao_entidade()
  {
    _mockRepository.Object.Delete(_dataBaseInput);
  }
  
  [Fact]
  public void teste_update_entidade()
  {
    _mockRepository.Object.Update(_dataBaseInput);
  }

  [Fact]
  public void teste_existente_entidade()
  {
    var obj = _mockRepository.Object.Exists(_dataBaseInput);
    Assert.NotNull(obj);
  }

  [Fact]
  public async Task teste_criacao_lista_entidade_assincrona()
  {
    await _mockRepository.Object.CreateListAsync(_listInput, _cancellationToken);
  }
  
  [Fact]
  public async Task teste_remocao_lista_entidade_assincrona()
  {
    await _mockRepository.Object.DeleteListAsync(_listInput, _cancellationToken);
  }
  [Fact]
  public async Task teste_update_lista_entidade_assincrona()
  {
    await _mockRepository.Object.UpdateListAsync(_listInput, _cancellationToken);
  }

  [Fact]
  public async Task teste_criacao_entidade_assincrona()
  {
    await _mockRepository.Object.CreateAsync(_dataBaseInput, _cancellationToken);
  }

  [Fact]
  public void teste_atualizacao_entidade_assincrona()
  {
    _mockRepository.Object.UpdateAsync(_dataBaseInput, _cancellationToken);
  }

  [Fact]
  public void teste_remocao_entidade_assincrona()
  {
    _mockRepository.Object.DeleteAsync(_dataBaseInput, _cancellationToken);
  }
}