using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
namespace Test.CharacterStats
{
    public class AdjustHealthSystem : SystemBase
    {
        EntityQuery _increaseHealth;
        EntityQuery _decreaseHealth;

        EntityQuery _increaseMana;
        EntityQuery _decreaseMana;
        EntityCommandBufferSystem _entityCommandBufferSystem;
        protected EntityCommandBufferSystem GetCommandBufferSystem()
        {
            return World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }
        protected override void OnCreate()
        {
            base.OnCreate();
            _increaseHealth = GetEntityQuery(new EntityQueryDesc()
            {
                All = new ComponentType[] { ComponentType.ReadWrite(typeof(Stats)), ComponentType.ReadWrite(typeof(IncreaseHealthTag)) }
            });
            _decreaseHealth = GetEntityQuery(new EntityQueryDesc()
            {
                All = new ComponentType[] { ComponentType.ReadWrite(typeof(Stats)), ComponentType.ReadWrite(typeof(DecreaseHealthTag)) }
            });
            _increaseMana = GetEntityQuery(new EntityQueryDesc()
            {
                All = new ComponentType[] { ComponentType.ReadWrite(typeof(Stats)), ComponentType.ReadWrite(typeof(IncreaseManaTag)) }
            });
            _decreaseMana = GetEntityQuery(new EntityQueryDesc()
            {
                All = new ComponentType[] { ComponentType.ReadWrite(typeof(Stats)), ComponentType.ReadWrite(typeof(DecreaseManaTag)) }
            });
            _entityCommandBufferSystem = GetCommandBufferSystem();
        }

        protected override void OnUpdate()
        {
            JobHandle systemDeps = Dependency;
            systemDeps = new IncreaseHealthJob()
            {
                DeltaTime = Time.DeltaTime,
                IncreaseChunk = GetArchetypeChunkComponentType<IncreaseHealthTag>(false),
                StatsChunk = GetArchetypeChunkComponentType<Stats>(false),
                EntityChunk = GetArchetypeChunkEntityType(),
                entityCommandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
            }.ScheduleParallel(_increaseHealth, systemDeps);

            systemDeps = new DecreaseHealthJob()
            {
                DeltaTime = Time.DeltaTime,
                DecreaseChunk = GetArchetypeChunkComponentType<DecreaseHealthTag>(false),
                StatsChunk = GetArchetypeChunkComponentType<Stats>(false),
                EntityChunk = GetArchetypeChunkEntityType(),
                entityCommandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
            }.ScheduleParallel(_decreaseHealth, systemDeps);
            _entityCommandBufferSystem.AddJobHandleForProducer(systemDeps);

            systemDeps = new IncreaseManaJob()
            {
                DeltaTime = Time.DeltaTime,
                IncreaseChunk = GetArchetypeChunkComponentType<IncreaseManaTag>(false),
                StatsChunk = GetArchetypeChunkComponentType<Stats>(false),
                EntityChunk = GetArchetypeChunkEntityType(),
                entityCommandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
            }.ScheduleParallel(_increaseMana, systemDeps);
            _entityCommandBufferSystem.AddJobHandleForProducer(systemDeps);

            systemDeps = new DecreaseManaJob()
            {
                DeltaTime = Time.DeltaTime,
                DecreaseChunk = GetArchetypeChunkComponentType<DecreaseManaTag>(false),
                StatsChunk = GetArchetypeChunkComponentType<Stats>(false),
                EntityChunk = GetArchetypeChunkEntityType(),
                entityCommandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
            }.ScheduleParallel(_decreaseMana, systemDeps);
            _entityCommandBufferSystem.AddJobHandleForProducer(systemDeps);

            Dependency = systemDeps;
        }


    }


    public struct IncreaseHealthJob : IJobChunk
    {
        public ArchetypeChunkComponentType<Stats> StatsChunk;
        public ArchetypeChunkComponentType<IncreaseHealthTag> IncreaseChunk;
        public float DeltaTime;
        [ReadOnly] public ArchetypeChunkEntityType EntityChunk;
        public EntityCommandBuffer.Concurrent entityCommandBuffer;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            NativeArray<Stats> stats = chunk.GetNativeArray<Stats>(StatsChunk);
            NativeArray<IncreaseHealthTag> healthChanges = chunk.GetNativeArray<IncreaseHealthTag>(IncreaseChunk);
            NativeArray<Entity> entities = chunk.GetNativeArray(EntityChunk);

            for (int i = 0; i < chunk.Count; i++)
            {
                Stats stat = stats[i];
                Entity entity = entities[i];
                IncreaseHealthTag healthChange = healthChanges[i];
                stat.CurHealth += healthChange.value;
                while (healthChange.Iterations>0) {
                    if (healthChange.Timer > 0.0f)
                    {
                        healthChange.Timer -= DeltaTime;
                    }
                    else {
                        healthChanges[i] = healthChange;
                        healthChange.Timer = healthChange.Frequency;
                        healthChange.Iterations--;
                    }
                }
                 entityCommandBuffer.RemoveComponent<IncreaseHealthTag>(chunkIndex, entity); 

            }
            
        }
    }

    public struct DecreaseHealthJob : IJobChunk
    {
        public ArchetypeChunkComponentType<Stats> StatsChunk;
        public ArchetypeChunkComponentType<DecreaseHealthTag> DecreaseChunk;
        public float DeltaTime;
        [ReadOnly] public ArchetypeChunkEntityType EntityChunk;
        public EntityCommandBuffer.Concurrent entityCommandBuffer;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            NativeArray<Stats> stats = chunk.GetNativeArray<Stats>(StatsChunk);
            NativeArray<DecreaseHealthTag> healthChanges = chunk.GetNativeArray<DecreaseHealthTag>(DecreaseChunk);
            NativeArray<Entity> entities = chunk.GetNativeArray(EntityChunk);

            for (int i = 0; i < chunk.Count; i++)
            {
                Stats stat = stats[i];
                Entity entity = entities[i];
                DecreaseHealthTag healthChange = healthChanges[i];
                stat.CurHealth -= healthChange.value;
                while (healthChange.Iterations > 0)
                {
                    if (healthChange.Timer > 0.0f)
                    {
                        healthChange.Timer -= DeltaTime;
                    }
                    else
                    {
                        healthChanges[i] = healthChange;
                        healthChange.Timer = healthChange.Frequency;
                        healthChange.Iterations--;
                    }
                }
                entityCommandBuffer.RemoveComponent<DecreaseHealthTag>(chunkIndex, entity);

            }
        }
    }

    public struct IncreaseManaJob : IJobChunk
    {
        public ArchetypeChunkComponentType<Stats> StatsChunk;
        public ArchetypeChunkComponentType<IncreaseManaTag> IncreaseChunk;
        public float DeltaTime;
        [ReadOnly] public ArchetypeChunkEntityType EntityChunk;
        public EntityCommandBuffer.Concurrent entityCommandBuffer;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            NativeArray<Stats> stats = chunk.GetNativeArray<Stats>(StatsChunk);
            NativeArray<IncreaseManaTag> manaChanges = chunk.GetNativeArray<IncreaseManaTag>(IncreaseChunk);
            NativeArray<Entity> entities = chunk.GetNativeArray(EntityChunk);

            for (int i = 0; i < chunk.Count; i++)
            {
                Stats stat = stats[i];
                Entity entity = entities[i];
                IncreaseManaTag manaChange = manaChanges[i];
                stat.CurMana += manaChange.value;
                while (manaChange.Iterations > 0)
                {
                    if (manaChange.Timer > 0.0f)
                    {
                        manaChange.Timer -= DeltaTime;
                    }
                    else
                    {
                        manaChanges[i] = manaChange;
                        manaChange.Timer = manaChange.Frequency;
                        manaChange.Iterations--;
                    }
                }
                entityCommandBuffer.RemoveComponent<DecreaseHealthTag>(chunkIndex, entity);

            }
        }
    }
    public struct DecreaseManaJob : IJobChunk
    {
        public ArchetypeChunkComponentType<Stats> StatsChunk;
        public ArchetypeChunkComponentType<DecreaseManaTag> DecreaseChunk;
        public float DeltaTime;
        [ReadOnly] public ArchetypeChunkEntityType EntityChunk;
        public EntityCommandBuffer.Concurrent entityCommandBuffer;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            NativeArray<Stats> stats = chunk.GetNativeArray<Stats>(StatsChunk);
            NativeArray<DecreaseManaTag> manaChanges = chunk.GetNativeArray<DecreaseManaTag>(DecreaseChunk);
            NativeArray<Entity> entities = chunk.GetNativeArray(EntityChunk);

            for (int i = 0; i < chunk.Count; i++)
            {
                Stats stat = stats[i];
                Entity entity = entities[i];
                DecreaseManaTag manaChange = manaChanges[i];
                stat.CurMana -= manaChange.value;
                while (manaChange.Iterations > 0)
                {
                    if (manaChange.Timer > 0.0f)
                    {
                        manaChange.Timer -= DeltaTime;
                    }
                    else
                    {
                        manaChanges[i] = manaChange;
                        manaChange.Timer = manaChange.Frequency;
                        manaChange.Iterations--;
                    }
                }
                entityCommandBuffer.RemoveComponent<DecreaseHealthTag>(chunkIndex, entity);

            }
        }
    }
}