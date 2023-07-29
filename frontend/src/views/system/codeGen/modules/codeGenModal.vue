<template>
  <a-modal :title="title" :width="900" :visible="visible" :confirmLoading="confirmLoading" @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="confirmLoading">
      <a-alert message="代码生成数据都是基于表格的列数据和备注信息,所以请在设计表格的时候,一定要规范设计并填写备注信息,否者可能会有问题" banner closable type="info" how-icon/>
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules" style="margin-top: 20px;">
        <a-row>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item prop="name" hasFeedback>
              <span slot="label">
                  <a-tooltip title="该名称,仅用于显示,跟代码生成无实际作用">
                    <a-icon type="question-circle-o" />
                  </a-tooltip>&nbsp;
                  名称
                </span>
              <a-input placeholder="请输入名称" v-model="form.name" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="表格名称" prop="tableName" hasFeedback>
              <a-select show-search v-model="form.tableName" :disabled="false" placeholder="请选择表格名称">
                <a-select-option v-for="(item, index) in tableNameData" :key="index" :value="item.name">{{
                  item.name
                }}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row>
        <!-- <a-row>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="所属应用" prop="menuType">
              <a-select style="width: 100%" v-model="form.menuType" placeholder="请选择应用分类">
                <a-select-option v-for="(item, index) in appData" :key="index" :value="item.value"
                  @click="changeApplication(item.value)">{{ item.name }}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row> -->
        <a-row>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="所属菜单" prop="menuId" hasFeedback>
              <a-tree-select style="width: 100%" v-model="form.menuId"
                :dropdownStyle="{ maxHeight: '300px', overflow: 'auto' }" :treeData="menuTreeData" :replaceFields="{
                  key: 'id',
                  title: 'name',
                  value: 'id'
                }" placeholder="请选择父级菜单" treeDefaultExpandAll>
                <span slot="title" slot-scope="{ title }">{{ title }}
                </span>
              </a-tree-select>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item prop="generationWay" label="生成方式" hasFeedback>
              <a-select v-model="form.generationWay">
                <a-select-option :value=0>项目生成</a-select-option>
                <a-select-option :value=1>打包生成</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import { sysDict } from '@/api/system'
export default {
  data() {
    return {
      gutter: 24,
      mdSize: 24,
      smSize: 24,
      title: '',
      formType: '',
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      visible: false,
      menuTreeData: [],
      tableNameData: [],
      confirmLoading: false,
      form: {},
      rules: {
        name: [{ required: true, message: '请输入名称！' }],
        tableName: [{ required: true, message: '请输入表格名称！' }],
        menuId: [{ required: true, message: '请输入所属菜单！' }],
        generationWay: [{ required: true, message: '请输入生成方式！' }]
      },
    }
  },
  methods: {
    // 初始化方法
    detail(record) {
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      if (record) {
        this.form = JSON.parse(JSON.stringify(record))
        this.formType = 'edit'
        this.title = '编辑数据'
      } else {
        this.form = createModal()
        this.formType = 'add'
        this.title = '新增数据'
      }
      this.visible = true
      this.getDropdown()
    },
    getDropdown() {
      this.$http.get('/sysCodeGen/getTableList').then(res => {
        if (res.code === 200) {
          this.tableNameData = res.result
        } else {
          this.$message.warning(res.message)
        }
      })
      this.$http.get('/sysMenu').then(res => {
        if (res.code === 200) {
          const menu = {
            id: 0,
            name: '不生成菜单',
            children: []
          }
          res.result.unshift(menu)
          this.menuTreeData = res.result
        } else {
          this.$message.warning(res.message)
        }
      })
    },

    close() {
      this.$emit('close')
      this.visible = false
    },
    handleOk() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.confirmLoading = true
          if (this.formType === 'edit') {
            this.$http.put('/sysCodeGen', this.form).then(res => {
              if (res.code === 200) {
                this.$emit('ok')
                this.visible = false
              } else {
                this.$message.warning(res.message)
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          }
          else {
            this.$http.post('/sysCodeGen', this.form).then(res => {
              if (res.code === 200) {
                this.$emit('ok')
                this.visible = false
              } else {
                this.$message.warning(res.message)
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          }
        }
      })
    },
    handleCancel() {
      this.close()
    }
  }
}
function createModal() {
  return {
    generationWay: 0,
    menuType: 'Business'
  }
}
</script>
