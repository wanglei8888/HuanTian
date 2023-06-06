<template>
  <a-modal :title="menuInfo.name+'权限按钮'" :width="1000" :visible="visible" :confirmLoading="confirmLoading" @ok="handleOk"
    @cancel="handleCancel">
    <a-row>
      <a-col :md="12" :sm="24" :style="{ marginTop: '22px' }">
        <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
          <a-form-model-item label="权限名字" prop="name" hasFeedback>
            <a-input placeholder="请输入权限名字" v-model="form.name" />
          </a-form-model-item>
          <a-form-model-item label="权限编码" prop="code" hasFeedback>
            <a-input placeholder="请输入权限编码" v-model="form.code" />
          </a-form-model-item>
          <a-form-model-item>
            <div style="text-align: right;">
              <a-button type="primary" icon="plus" @click="addTable()">新建</a-button>
            </div>
          </a-form-model-item>
        </a-form-model>
      </a-col>
      <a-col :md="12" :sm="24" :style="{ marginTop: '22px' }">
        <a-table :columns="columns" :dataSource="loadData" :pagination="false" :loading="false"
          :rowKey="record => record.key" :scroll="{ y: 162 }">
          <template slot="operation" slot-scope="text, record">
            <span>
              <a-popconfirm title="是否要删除此行？" @confirm="remove(record.id)">
                <a>删除</a>
              </a-popconfirm>
            </span>
            <span>
              <a-divider type="vertical" />
              <a @click="edit(record)">编辑</a>
            </span>
          </template>
        </a-table>
      </a-col>
    </a-row>

  </a-modal>
</template>

<script>
import { sysDict } from '@/api/system'
export default {
  components: {
  },
  data() {
    this.columns = columns
    return {
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 12 },
        sm: { span: 20 }
      },
      gutter: 24,
      mdSize: 12,
      smSize: 24,
      visible: false,
      confirmLoading: false,
      menuInfo: {},
      loadData: [],
      form: {},
      rules: {
        name: [{ required: true, min: 1, message: '请输入按钮名称！' }],
        code: [{ required: true, min: 1, message: '请输入权限编码！' }]
      },
      expandedKeys: [],
      autoExpandParent: true
    }
  },
  methods: {
    onExpand(expandedKeys) {
      this.expandedKeys = expandedKeys;
      this.autoExpandParent = false;
    },
    edit(record) {
      this.form = JSON.parse(JSON.stringify(record))
    },
    remove(id) {
      this.loadData = this.loadData.filter((item) => item.id !== id)
    },
    // 打开页面初始化
    create(value) {
      this.menuInfo = value
      this.visible = true
      this.confirmLoading = false
      this.loadData = []
      this.handSerach()
    },
    // 查询角色菜单信息
    handSerach() {
      // 查询菜单存在的权限
      this.$http.get('/sysPermissions', { params: { menuId: this.menuInfo.id } }).then(res => {
        this.loadData = res.result
      }).catch(() => {
        this.$message.error(res.message)
      })
    },
    addTable() {
      const _this = this
      // 生成一个不重复的id
      const id = Math.round(Math.random() * 1000) + (new Date()).getTime()
      this.$refs.form.validate(valid => {
        if (valid) {
          // 遍历loadData中数据，如果code相同，就提示不能重复添加
          const isExist = _this.loadData.some(item => item.code === _this.form.code)
          if (isExist) {
            _this.$message.warning('权限编码不能重复')
            return
          }
          // 查询loadData中id如果相同，就修改这条数据
          const index = _this.loadData.findIndex(item => item.id === _this.form.id)
          if (index !== -1) {
            _this.loadData[index].name = _this.form.name
            _this.loadData[index].code = _this.form.code
            _this.form.id = ''
          } else {
            this.loadData.push({
              id: id,
              name: _this.form.name,
              code: _this.form.code,
              menuId: _this.menuInfo.id,
              type: 0
            })
          }
        }
      })
    },
    close() {
      this.$emit('close')
      this.visible = false
    },
    handleOk() {
      if (this.loadData.length === 0) {
        this.$message.warning('请添加权限表格信息')
        return
      }
      this.confirmLoading = true
      this.$http.post('/SysPermissions', this.loadData).then(res => {
        if (res.code === 200) {
          this.$emit('ok')
          this.confirmLoading = false
          this.visible = false
        } else {
          this.$message.warning(res.message)
        }
      })
    },
    handleCancel() {
      this.close()
    },
  },
}
const columns = [
  {
    title: '权限名称',
    dataIndex: 'name',
    key: 'name',
    width: '32%'
  },
  {
    title: '权限编码',
    dataIndex: 'code',
    key: 'code',
    width: '32%'
  },
  {
    title: '操作',
    width: '32%',
    key: 'action',
    scopedSlots: { customRender: 'operation' }
  }
]
</script>

<style scoped lang="less"></style>
